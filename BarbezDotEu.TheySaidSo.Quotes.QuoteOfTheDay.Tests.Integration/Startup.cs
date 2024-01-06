// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using BarbezDotEu.TheySaidSo.Quotes.QuoteOfTheDay.Tests.Integration.Factories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BarbezDotEu.TheySaidSo.Quotes.QuoteOfTheDay.Tests.Integration
{
    public class Startup
    {
        public Startup()
        {
            var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Logs/BarbezDotEu.TheySaidSo.Quotes.QuoteOfTheDay.Tests.Integration-.log");
            Log.Logger = new LoggerConfiguration()
                .Enrich
                .FromLogContext()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddHttpClient();
            services.AddSingleton<IMemoryCache, MemoryCache>();
            services.AddTransient<IQuotesRestClientFactory, QuotesRestClientFactory>();
        }

        public void ConfigureHost(IHostBuilder hostBuilder) =>
            hostBuilder
                .UseSerilog()
                .ConfigureHostConfiguration(builder =>
                {
                    builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureAppConfiguration((context, builder) => { });
    }
}
