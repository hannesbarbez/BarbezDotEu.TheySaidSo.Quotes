// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

namespace BarbezDotEu.TheySaidSo.Quotes.QuoteOfTheDay
{
    /// <summary>
    /// Implements and houses configuration parameters to correctly connect to and communicate with quotes.rest's services.
    /// </summary>
    /// <remarks>
    /// Constructs a new <see cref="QuotesRestClientConfiguration"/> using given parameters.
    /// </remarks>
    /// <param name="maxCallsPerDay">The maximum number of calls allowed per hour (see https://quotes.rest/ for current rate limits).</param>
    /// <param name="apiKey">The API Key you got via https://theysaidso.com/api.</param>
    /// <param name="language">The language in which quotes are to be returned.</param>
    public class QuotesRestClientConfiguration(long maxCallsPerDay, string apiKey, string language = "en")
    {
        private static readonly string _quoteOfTheDayUrl = "https://quotes.rest/qod?language={0}";

        /// <summary>
        /// Gets the maximum number of calls allowed per hour (see https://quotes.rest/ for current rate limits).
        /// </summary>
        public long MaxCallsPerDay { get; } = maxCallsPerDay;

        /// <summary>
        /// Gets the URL for querying the quote of the day.
        /// </summary>
        public string QuoteOfTheDayUrl { get; } = string.Format(_quoteOfTheDayUrl, language);

        /// <summary>
        /// Gets the API Key to authenticate requests.
        /// </summary>
        public string ApiKey { get; } = apiKey;
    }
}
