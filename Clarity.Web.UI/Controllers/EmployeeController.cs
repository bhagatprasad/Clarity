using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.Organization.Client;
using Newtonsoft.Json;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly INotyfService notyfService;
        private static readonly ILog log = LogManager.GetLogger(typeof(EmployeeController));
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
            try
            {
                if (addEditEmployee != null)
                {
                    var responce = await employeeService.InsertOrUpdateAsync(addEditEmployee);

                    var employees = await employeeService.fetchAllEmployeesAsync();
                    log.Info(JsonConvert.SerializeObject(employees));
                    notyfService.Success("Employee On Boarding completed");

                    return Json(new { data = employees });
                }

                notyfService.Success("Somethingwent wrong");

                return Json(false);
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                log.Error("InsertOrUpdateEmployee.." + ex);
                throw ex;
            }

        }
        [HttpPost]
        public async Task<IActionResult> EmployeeSalaryHike([FromBody] SalaryHike salaryHike)
        {
            try
            {
                if (salaryHike != null)
                {
                    var responce = await employeeService.SalaryHikeAsync(salaryHike);

                    notyfService.Success("Employeesalary structes was changed and hike was issued");

                    return Json(new { data = responce });
                }

                notyfService.Success("Somethingwent wrong");

                return Json(false);
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                log.Error("EmployeeSalaryHike.." + ex);
                throw ex;
            }

        }
        [HttpGet]
        public async Task<IActionResult> fetchAllEmployess()
        {
            try
            {
                var employees = await employeeService.fetchAllEmployeesAsync();
                log.Info(JsonConvert.SerializeObject(employees));
                return Json(new { data = employees });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                log.Error("fetchAllEmployess.." + ex);
                throw ex;
            }

        }
    }
}
