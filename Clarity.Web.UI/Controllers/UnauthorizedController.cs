using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    public class UnauthorizedController : Controller
    {
        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
