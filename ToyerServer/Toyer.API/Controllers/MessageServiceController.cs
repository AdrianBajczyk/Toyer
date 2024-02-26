using Microsoft.AspNetCore.Mvc;
using Toyer.Logic.Responses;
using Toyer.Logic.Services.Repositories.Interfaces;

namespace Toyer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class MessageServiceController(IMessageServiceRepository messageServiceRepository) : ControllerBase
    {
        private readonly IMessageServiceRepository _messageServiceRepository = messageServiceRepository;

        /// <summary>
        /// Send a message to service's email box.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomResponse), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateOrderAsync([FromBody] EmailMessageDataDto messageDataDto)
        {

            await _messageServiceRepository.SendEmailMessage(messageDataDto.ContactEmail, "toyer.service@gmail.com", messageDataDto.Message);

            return NoContent();
        }
    }
}
