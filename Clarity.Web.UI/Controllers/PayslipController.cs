using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Admin")]
    public class PayslipController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
