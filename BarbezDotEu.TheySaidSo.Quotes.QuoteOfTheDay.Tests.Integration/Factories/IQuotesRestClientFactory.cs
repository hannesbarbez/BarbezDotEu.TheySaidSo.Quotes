// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using BarbezDotEu.TheySaidSo.Quotes.QuoteOfTheDay.Interfaces;

namespace BarbezDotEu.TheySaidSo.Quotes.QuoteOfTheDay.Tests.Integration.Factories
{
    /// <summary>
    /// Defines an interface for a factory producing <see cref="IQuotesRestClient"/>s.
    /// </summary>
    public interface IQuotesRestClientFactory
    {
        /// <summary>
        /// Creates an <see cref="IQuotesRestClient"/>.
        /// </summary>
        /// <returns>An instance of <see cref="IQuotesRestClient"/>.</returns>
        IQuotesRestClient Create();
    }
}
