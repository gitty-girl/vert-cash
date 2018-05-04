using System.Net;
using System.Threading.Tasks;
using CurrencyConverter.ExecutionControl;
using CurrencyConverter.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.APIs
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/converter")]
    public class ConverterController : Controller
    {
        private readonly ConverterService _service;

        public ConverterController()
        {
            _service = new ConverterService();
        }

        /// <summary>
        /// Gets a list of all leads
        /// </summary>
        /// /// <param name="currencyCode">3 digit currency code</param>
        /// /// <param name="amount">Amount</param>
        /// <response code="400">Bad request, see collection of errors for details</response>
        /// <response code="500">Internal server error, see message for details.</response>
        [HttpGet]
        [Route("/ConvertToEuro")]
        [ProducesResponseType(typeof(double), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ConvertToEuro(string currencyCode, double amount) =>
            await _service.ConvertEuro(currencyCode, amount).ToHttpResponseAsync();
    }
}