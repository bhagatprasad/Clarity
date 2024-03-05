using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    [Authorize]
    public class PayslipController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
