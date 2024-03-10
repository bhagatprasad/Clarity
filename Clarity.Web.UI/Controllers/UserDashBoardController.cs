using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Software Engineer/Developer,Full-stack Developer")]
    public class UserDashBoardController : Controller
    {
        public UserDashBoardController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
