using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.Account;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Admin")]
    public class MonthlySalaryController : Controller
    {
        private readonly IMonthlySalaryService monthlySalaryService;
        private readonly INotyfService notyfService;
        public MonthlySalaryController(IMonthlySalaryService monthlySalaryService,
            INotyfService notyfService)
        {
            this.monthlySalaryService = monthlySalaryService;
            this.notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PublishMonthlySalary([FromBody] MonthlySalary monthlySalary)
        {
            try
            {
                if (monthlySalary != null)
                {
                    var responce = await monthlySalaryService.PublishMonthlySalary(monthlySalary);

                    if (responce)
                        notyfService.Success("Salary for the month of" + monthlySalary.SalaryMonth + "_" + monthlySalary.SalaryYear + "was published to all employees are done");
                    return Json(responce);
                }

                notyfService.Error("somethingwent wrong");
                return Json(false);
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> fetchAllMonthlySalaries()
        {
            try
            {
                var salaries = await monthlySalaryService.fetchAllMonthlySalaries();
                return Json(new { data = salaries });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
