// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

namespace BarbezDotEu.TheySaidSo.Quotes.QuoteOfTheDay.DTOs
{
    /// <summary>
    /// Implements a list of quotes.
    /// </summary>
    public class Contents
    {
        /// <summary>
        /// Gets or sets a list of quotes.
        /// </summary>
        public ICollection<SingleQuote>? Quotes { get; set; }
    }
}
