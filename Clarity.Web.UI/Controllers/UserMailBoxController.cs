using Clarity.Web.UI.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Software Engineer/Developer,Full-stack Developer")]
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
        public async Task<IActionResult> GetAllUserMailBoxes(long userId)
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
