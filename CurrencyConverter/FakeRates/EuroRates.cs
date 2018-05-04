using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyConverter.FakeRates
{
    public class EuroRates
    {
        private readonly Dictionary<string, double> _rate =
            new Dictionary<string, double>
            {
                { "USD", 1.1992 },
                { "JPY", 131.03 },
                { "BGN", 1.9558 },
                { "CZK", 25.585 },
                { "DKK", 7.4497 },
                { "GBP", 0.88180 },
                { "HUF", 314.06 },
                { "PLN", 4.2628 },
                { "RON", 4.6658 },
                { "SEK", 10.6045 },
                { "CHF", 1.1958 },
                { "ISK", 122.20 },
                { "NOK", 9.6465 },
                { "HRK", 7.4153 },
                { "RUB", 76.0875 },
                { "TRY", 5.0360 },
                { "AUD", 1.5926 },
                { "BRL", 4.2367 },
                { "CAD", 1.5404 },
                { "CNY", 7.6135 },
                { "HKD", 9.4131 },
                { "IDR", 16708.75 },
                { "ILS", 4.3519 },
                { "INR", 79.8965 },
                { "KRW", 1288.96 },
                { "MXN", 22.8159 },
                { "MYR", 4.7094 },
                { "NZD", 1.7041 },
                // God have mercy on my soul
                { "PHP", 61.995 },
                { "SGD", 1.5962 },
                { "THB", 37.931 },
                { "ZAR", 15.1399 }
            };

        public async Task<double> GetRate(string currencyCode)
        {
            if (_rate.TryGetValue(currencyCode, out var result))
                return result;

            throw new Exception("Currency with given code not found");
        }
    }
}