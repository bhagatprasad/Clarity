using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    [Authorize]
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
