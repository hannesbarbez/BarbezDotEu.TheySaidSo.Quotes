// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Mime;
using BarbezDotEu.Provider;
using BarbezDotEu.TheySaidSo.Quotes.QuoteOfTheDay.Interfaces;
using BarbezDotEu.TheySaidSo.Quotes.QuoteOfTheDay.Responses;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace BarbezDotEu.TheySaidSo.Quotes.QuoteOfTheDay
{
    /// <summary>
    /// Implements a client that connects to and communicates with quotes.rest's services.
    /// </summary>
    public class QuotesRestClient : PoliteProvider, IQuotesRestClient
    {
        private readonly MemoryCacheEntryOptions _expireOutsideOfRateLimitRange;
        private readonly MediaTypeWithQualityHeaderValue _acceptHeader;
        private readonly QuotesRestClientConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Constructs a <see cref="QuotesRestClient"/>.
        /// </summary>
        /// <param name="logger">A <see cref="ILogger"/> to use for logging.</param>
        /// <param name="httpClientFactory">The <see cref="IHttpClientFactory"/> to use.</param>
        /// <param name="memoryCache">The local in-memory cache whose values are not serialized.</param>
        /// <param name="configuration">The <see cref="QuotesRestClientConfiguration"/> to configure this <see cref="IQuotesRestClient"/> with.</param>
        public QuotesRestClient(ILogger logger, IHttpClientFactory httpClientFactory, IMemoryCache memoryCache, QuotesRestClientConfiguration configuration)
            : base(logger, httpClientFactory)
        {
            this._acceptHeader = new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json);
            this._configuration = configuration;
            this.SetRateLimitPerDay(this._configuration.MaxCallsPerDay);
            this._expireOutsideOfRateLimitRange = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(RequiredSecondsInBetweenCalls + 1));
            this._memoryCache = memoryCache;
        }

        /// <inheritdoc/>
        public async Task<PoliteReponse<QuoteOfTheDayResponse>> GetQuoteOfTheDay()
        {
            var cacheKey = typeof(QuotesRestClient).FullName + nameof(GetQuoteOfTheDay);
            if (!this._memoryCache.TryGetValue(cacheKey, out PoliteReponse<QuoteOfTheDayResponse>? quoteOfTheDayResponse))
            {
                ValidateRequest();
                quoteOfTheDayResponse = await Get<QuoteOfTheDayResponse>(_configuration.QuoteOfTheDayUrl);
                if (!quoteOfTheDayResponse.HasFailed)
                    this._memoryCache.Set(cacheKey, quoteOfTheDayResponse, _expireOutsideOfRateLimitRange);
            }

            if (quoteOfTheDayResponse == null)
                throw new UnreachableException();

            return quoteOfTheDayResponse;
        }

        /// <remarks>
        /// If we were to implement the entire (Quotes.Rest) APIs, we'd have Post, (Delete, Put, theoretically) etc methods here to really use generics.
        /// </remarks>
        private async Task<PoliteReponse<T>> Get<T>(string url)
            where T : class
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Accept.Add(_acceptHeader);
            request.Headers.Add("X-TheySaidSo-Api-Secret", _configuration.ApiKey);
            var result = await this.Request<T>(request, retryOnError: false);
            return result;
        }

        private void ValidateRequest()
        {
            // If cache is configured properly vis-a-vis rate limit, we shouldn't end up here.
            if (!base.IsPolite())
            {
                Logger.LogError("{nameofQuotesRestClient} did not request a new {nameofGetQuoteOfTheDay} because it would not respect the rate limit set by the data provider. At the same time, a cached version of it is not available.", nameof(QuotesRestClient), nameof(GetQuoteOfTheDay));
                throw new InvalidOperationException();
            }
        }
    }
}
