using Clarity.Web.Service.Helpers;
using Clarity.Web.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageTypeController : ControllerBase
    {
        private readonly IMessageTypeService messageTypeService;

        public MessageTypeController(IMessageTypeService _messageTypeService)
        {
            this.messageTypeService = _messageTypeService;
        }
        [HttpGet]
        [Route("FetchAllMessageType")]
        public async Task<IActionResult> FetchAllMessageType()
        {
            try
            {
                var response = await messageTypeService.FetchAllMessageType();
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
