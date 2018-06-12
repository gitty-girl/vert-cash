using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CurrencyConverter.ExecutionControl;
using CurrencyConverter.Models;
using CurrencyConverter.Models.Dtos;
using CurrencyConverter.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.APIs
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets single user infromation by it's Id
        /// </summary>
        /// <param name="id">id of the user</param>
        /// <response code="400">Bad request, see collection of errors for details</response>
        /// <response code="500">Internal server error, see message for details.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] int id) =>
            await _service.GetUserAsync(id).ToHttpResponseAsync();

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="dto">Data needed for user creation</param>
        /// <response code="400">Bad request, see collection of errors for details</response>
        /// <response code="500">Internal server error, see message for details.</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post([FromBody] UserDto dto) =>
            await _service.AddUserAsync(dto).ToHttpResponseAsync();

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="id">id of the user</param>
        /// <param name="dto">Data needed for user update</param>
        /// <response code="400">Bad request, see collection of errors for details</response>
        /// <response code="500">Internal server error, see message for details.</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]UserDto dto) =>
            await _service.UpdateUserAsync(id, dto).ToHttpResponseAsync();

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id">id of the user</param>
        /// <response code="400">Bad request, see collection of errors for details</response>
        /// <response code="500">Internal server error, see message for details.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id) =>
            await _service.DeleteUser(id).ToHttpResponseAsync();

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