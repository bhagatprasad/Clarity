using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly INotyfService notyfService;
        public EmployeeController(IEmployeeService employeeService,
             INotyfService notyfService)
        {
            this.employeeService = employeeService;
            this.notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateEmployee([FromBody] AddEditEmployee addEditEmployee)
        {
            if (addEditEmployee != null)
            {
                var responce = await employeeService.InsertOrUpdateAsync(addEditEmployee);

                var employees = await employeeService.fetchAllEmployeesAsync();

                notyfService.Success("Employee On Boarding completed");

                return Json(new { data = employees });
            }

            notyfService.Success("Somethingwent wrong");

            return Json(false);
        }
        [HttpGet]
        public async Task<IActionResult> fetchAllEmployess()
        {
            var employees = await employeeService.fetchAllEmployeesAsync();
            return Json(new { data = employees });

        }
    }
}
