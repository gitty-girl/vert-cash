using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyConverter.ExecutionControl;
using CurrencyConverter.FakeRates;

namespace CurrencyConverter.Services
{
    public class ConverterService
    {
        private readonly EuroRates _euroRates;

        public ConverterService()
        {
            _euroRates = new EuroRates();
        }

        public async Task<ExecutionResult<double>> ConvertEuro(string currencyCode, double amount)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(currencyCode) || currencyCode.Length != 3 || amount < 0)
                    throw new Exception("Invalid parameters");

                var result = await _euroRates.GetRate(currencyCode.ToUpper());
                return ExecutionResult<double>.Success(result * amount);
            }
            catch (Exception ex)
            {
                return new ExecutionResult<double>(ExecutionResult.DomainFailedResult(new Dictionary<string, string>
                {
                    { "Bad Request", ex.Message }
                }));
            }
            
        }
    }
}