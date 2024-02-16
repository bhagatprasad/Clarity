using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    public class RolesController : Controller
    {
        
        public RolesController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
