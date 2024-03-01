using Clarity.Web.UI.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    [Authorize]
    public class DesignationController : Controller
    {
        private readonly IDesignationService designationService;
        public DesignationController(IDesignationService designationService)
        {
            this.designationService = designationService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> fetchAllDesignations()
        {
            var designations = await designationService.GetAllDesignation();
            return Json(new { data = designations });
        }
    }
}
