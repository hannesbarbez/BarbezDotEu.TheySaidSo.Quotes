// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using BarbezDotEu.TheySaidSo.Quotes.QuoteOfTheDay.Tests.Integration.Factories;
using FluentAssertions;

namespace BarbezDotEu.TheySaidSo.Quotes.QuoteOfTheDay.Tests.Integration
{
    /// <summary>
    /// Implements happy flow tests.
    /// </summary>
    [Trait("Integration", "QuotesRest")]
    public class QuotesRestClientShould(IQuotesRestClientFactory quotesRestClientFactory)
    {
        private readonly IQuotesRestClientFactory _quotesRestClientFactory = quotesRestClientFactory;

        [Fact]
        public async void GetQuoteOfTheDay()
        {
            // Arrange
            var client = _quotesRestClientFactory.Create();

            // Act
            var response = await client.GetQuoteOfTheDay();

            // Assert
            response.Should().NotBeNull();
            response.HasFailed.Should().BeFalse();
            response.Content.Should().NotBeNull();
            response.Content.Success.Should().NotBeNull();
            response.Content.Success!.Total.Should().Be(1);
            response.Content.Contents.Should().NotBeNull();
            response.Content.Contents!.Quotes.Should().NotBeNull();
            response.Content.Contents.Quotes!.Count.Should().Be(1);
        }
    }
}
