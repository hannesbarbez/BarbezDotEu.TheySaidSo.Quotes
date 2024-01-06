// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using BarbezDotEu.TheySaidSo.Quotes.QuoteOfTheDay.DTOs;

namespace BarbezDotEu.TheySaidSo.Quotes.QuoteOfTheDay.Responses
{
    /// <summary>
    /// Implements the response expected from the quote of the day service.
    /// Use this to get the quote of the day in various categories.
    /// This is a free API that is available to public.
    /// You must credit They Said So if you are using the free version.
    /// </summary>
    public class QuoteOfTheDayResponse
    {
        /// <summary>
        /// Gets or sets metadata about this successful call.
        /// </summary>
        public Success? Success { get; set; }

        /// <summary>
        /// Gets or sets the list of quotes.
        /// </summary>
        public Contents? Contents { get; set; }

        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        public string? Baseurl { get; set; }

        /// <summary>
        /// Gets or sets copyright metadata.
        /// </summary>
        public Copyright? Copyright { get; set; }
    }
}
