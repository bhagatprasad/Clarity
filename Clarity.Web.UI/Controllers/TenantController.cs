using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.VisualStudio.Services.Graph.GraphResourceIds;

namespace Clarity.Web.UI.Controllers
{
    [Authorize]
    public class TenantController : Controller
    {
        private readonly ITenantService tenantService;
        private readonly IEmployeeSalaryService employeeSalaryService;
        private readonly IEmployeeService employeeService;
        private readonly INotyfService notyfService;
        public TenantController(ITenantService tenantService, IEmployeeSalaryService employeeSalaryService,
            IEmployeeService employeeService,
            INotyfService notyfService)
        {
            this.tenantService = tenantService;
            this.employeeSalaryService = employeeSalaryService;
            this.employeeService = employeeService;
            this.notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUser registerUser)
        {
            if (registerUser != null)
            {
                var responce = await tenantService.fnRegisterUserAsync(registerUser);

                if (responce)
                {
                    var employees = await employeeService.fetchAllEmployeesAsync();

                    var users = await tenantService.fetchUsers();

                    notyfService.Success("Credentials created");

                    return Json(new { employees = employees, users = users });
                }
            }

            notyfService.Error("Somethingwent wrong");

            return Json(new { employees = new List<AddEditEmployee>(), users = new List<User>() });
        }

        [HttpGet]
        public async Task<IActionResult> fetchUsers()
        {
            var users = await tenantService.fetchUsers();
            return Json(new { data = users });
        }
    }
}
