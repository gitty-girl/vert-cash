using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CurrencyConverter.ExecutionControl;
using CurrencyConverter.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.APIs
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/author")]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Seeds default users
        /// </summary>
        /// <response code="400">Bad request, see collection of errors for details</response>
        /// <response code="500">Internal server error, see message for details.</response>
        [HttpPost("seed")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Seed() =>
            await _service.Seed().ToHttpResponseAsync();
    }
}