// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using BarbezDotEu.Provider;
using BarbezDotEu.Provider.Interfaces;
using BarbezDotEu.TheySaidSo.Quotes.QuoteOfTheDay.Responses;

namespace BarbezDotEu.TheySaidSo.Quotes.QuoteOfTheDay.Interfaces
{
    /// <summary>
    /// Defines a blueprint for a client that connects to and communicates with quotes.rest's services.
    /// </summary>
    public interface IQuotesRestClient : IPoliteProvider
    {
        /// <summary>
        /// Queries for the quote of the day.
        /// </summary>
        /// <returns>A <see cref="PoliteReponse{T}"/> containing the <see cref="QuoteOfTheDayResponse"/>.</returns>
        Task<PoliteReponse<QuoteOfTheDayResponse>> GetQuoteOfTheDay();
    }
}
