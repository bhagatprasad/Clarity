using Clarity.Web.UI.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Admin,Software Engineer/Developer,Full-stack Developer")]
    public class MessageTypeController : Controller
    {
        private readonly IMessageTypeService messageService;
        public MessageTypeController(IMessageTypeService _messageTypeService)
        {
            this.messageService = _messageTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
           
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllMessageType()
        {
            try
            {
                var messagetype = await messageService.GetAllMessageTypes();
                return Json(new { data = messagetype });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
