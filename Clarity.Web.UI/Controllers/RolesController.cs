using Clarity.Web.UI.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRolesService rolesService;
        public RolesController(IRolesService rolesService)
        {
            this.rolesService = rolesService;
        }
        public async Task<IActionResult> Index()
        {
            //var responce = await rolesService.fetchAllRoles();

            return View();
        }
    }
}
