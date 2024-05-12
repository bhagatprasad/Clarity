using Clarity.Web.UI.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    public class MessageTypeController : Controller
    {
        private readonly IMessageTypeService messageService;
        public MessageTypeController(IMessageTypeService _messageTypeService)
        {
            this.messageService = _messageTypeService;
        }
        public IActionResult Index()
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
