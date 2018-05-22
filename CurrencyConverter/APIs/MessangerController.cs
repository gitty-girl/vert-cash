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
    [Route("api/messanger")]
    public class MessangerController : Controller
    {
        private readonly IMessangerService _service;

        public MessangerController(IMessangerService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets single message infromation by it's Id
        /// </summary>
        /// <param name="id">id of the message</param>
        /// <response code="400">Bad request, see collection of errors for details</response>
        /// <response code="500">Internal server error, see message for details.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Message), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] int id) =>
            await _service.GetMessageAsync(id).ToHttpResponseAsync();

        /// <summary>
        /// Creates a message
        /// </summary>
        /// <param name="dto">Data needed for message creation</param>
        /// <response code="400">Bad request, see collection of errors for details</response>
        /// <response code="500">Internal server error, see message for details.</response>
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post([FromBody] MessageDto dto) =>
            await _service.PostMessageAsync(dto).ToHttpResponseAsync();


        /// <summary>
        /// Updates a message
        /// </summary>
        /// <param name="id">id of the message</param>
        /// <param name="dto">Data needed for message update</param>
        /// <response code="400">Bad request, see collection of errors for details</response>
        /// <response code="500">Internal server error, see message for details.</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]MessageDto dto) =>
            await _service.EditMessageAsync(id, dto).ToHttpResponseAsync();

        /// <summary>
        /// Deletes a lead
        /// </summary>
        /// <param name="id">id of the lead</param>
        /// <response code="400">Bad request, see collection of errors for details</response>
        /// <response code="500">Internal server error, see message for details.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id) =>
            await _service.DeleteMessageAsync(id).ToHttpResponseAsync();

    }
}