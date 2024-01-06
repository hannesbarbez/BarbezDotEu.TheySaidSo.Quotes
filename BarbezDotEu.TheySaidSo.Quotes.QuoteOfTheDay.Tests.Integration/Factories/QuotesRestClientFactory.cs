// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using BarbezDotEu.TheySaidSo.Quotes.QuoteOfTheDay.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BarbezDotEu.TheySaidSo.Quotes.QuoteOfTheDay.Tests.Integration.Factories
{
    /// <summary>
    /// Implements a factory producing <see cref="IQuotesRestClient"/>s.
    /// </summary>
    public class QuotesRestClientFactory : IQuotesRestClientFactory
    {
        private readonly ILogger<IQuotesRestClient> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly QuotesRestClient _client;

        /// <summary>
        /// Constructs a <see cref="QuotesRestClientFactory"/>.
        /// </summary>
        /// <param name="logger">A <see cref="ILogger"/> to use for logging.</param>
        /// <param name="httpClientFactory">The <see cref="IHttpClientFactory"/> to use.</param>
        /// <param name="configuration">An instance of <see cref="IConfiguration"/>.</param>
        /// <param name="memoryCache">An instance of <see cref="IMemoryCache"/>.</param>
        public QuotesRestClientFactory(ILogger<IQuotesRestClient> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration, IMemoryCache memoryCache)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this._memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            var clientConfiguration = new QuotesRestClientConfiguration(GetMaxCallsPerDay(), GetApiToken());
            this._client = new QuotesRestClient(_logger, _httpClientFactory, _memoryCache, clientConfiguration);
        }

        /// <inheritdoc/>
        public IQuotesRestClient Create()
        {
            return _client;
        }

        private long GetMaxCallsPerDay()
        {
            var maxCallsPerDayString = this._configuration["QuotesRest:MaxCallsPerDay"];
            var parsed = long.TryParse(maxCallsPerDayString, out long maxCallsPerDay);
            if (!parsed)
                maxCallsPerDay = 5;

            return maxCallsPerDay;
        }

        private string GetApiToken()
        {
            var apiToken = this._configuration["QuotesRest:ApiToken"];
            if (apiToken == null)
                throw new ArgumentNullException(nameof(apiToken));

            return apiToken;
        }
    }
}
