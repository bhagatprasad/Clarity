using Clarity.Web.UI.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    public class UserMailBoxController : Controller
    {
        private readonly IUserMailBoxService userMailBoxService;

        public UserMailBoxController(IUserMailBoxService _userMailBoxService)
        {
            this.userMailBoxService = _userMailBoxService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUserMailBoxAsync()
        {
            try
            {
                var mailbox = await userMailBoxService.GetAllUserMailBoxAsync();
                return Json(new { data = mailbox });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserMailBoxAsync(long userId)
        {
            try
            {
                var mailbox = await userMailBoxService.GetAllUserMailBoxAsync(userId);
                return Json(new { data = mailbox });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
