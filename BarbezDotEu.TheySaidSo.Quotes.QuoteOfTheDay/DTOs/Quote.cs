// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

namespace BarbezDotEu.TheySaidSo.Quotes.QuoteOfTheDay.DTOs
{
    /// <summary>
    /// Implements a single quote and its metadata.
    /// </summary>
    public class SingleQuote
    {
        /// <summary>
        /// Gets or sets the quote.
        /// </summary>
        public string? Quote { get; set; }

        /// <summary>
        /// Gets or sets the length of the quote string.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the author name of the quote.
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// Gets or sets a list of tags/categories.
        /// </summary>
        public object? Tags { get; set; }

        /// <summary>
        /// Gets or sets the quote category.
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Gets or sets the language of the quote.
        /// </summary>
        public string? Language { get; set; }

        /// <summary>
        /// Gets or sets the date this quote of the day belongs to.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets a permalink pointing to this quote directly.
        /// </summary>
        public string? Permalink { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier representing a specific quote in QuotesRest.com.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the URI to the background image accompanying this quote.
        /// </summary>
        public string? Background { get; set; }

        /// <summary>
        /// Gets or sets the title accompanying this quote.
        /// </summary>
        public string? Title { get; set; }
    }
}
