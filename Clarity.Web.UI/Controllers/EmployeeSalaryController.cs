using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    public class EmployeeSalaryController : Controller
    {
        private readonly IEmployeeSalaryService employeeSalaryService;
        private readonly INotyfService notyfService;

        public EmployeeSalaryController(IEmployeeSalaryService _employeeSalaryService, INotyfService _notyfService)
        {
            this.employeeSalaryService = _employeeSalaryService;
            this.notyfService = _notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public async Task<IActionResult> FetchAllEmployeeSalaries()
        {
            var salaries = await employeeSalaryService.FetchAllEmployeeSalaries("", "", "", "", 0);
            return Json(new { data = salaries });
        }

        [HttpGet]
        public async Task<IActionResult> FetchEmployeeSalary (long employeeSalaryId)
        {
            var salaries = await employeeSalaryService.FetchEmployeeSalary(employeeSalaryId);
            return Json(new { data = salaries });
        }
    }
}
