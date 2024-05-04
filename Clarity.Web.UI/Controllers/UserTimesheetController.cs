using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Software Engineer/Developer,Full-stack Developer")]
    public class UserTimesheetController : Controller
    {
        private readonly ITimesheetService timesheetService;
        private readonly INotyfService notyfService;
        private readonly IEmployeeService employeeService;
        private readonly ITaskItemService taskItemService;
        private readonly ITaskCodeService taskCodeService;
        public UserTimesheetController(ITimesheetService timesheetService,
            INotyfService notyfService,
            IEmployeeService employeeService,
            ITaskItemService taskItemService,
            ITaskCodeService taskCodeService)
        {
            this.notyfService = notyfService;
            this.timesheetService = timesheetService;
            this.employeeService = employeeService;
            this.taskItemService = taskItemService;
            this.taskCodeService = taskCodeService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> fetchAllUserTimesheets(long userId)
        {
            try
            {
                var responce = await timesheetService.GetAllTimesheetsAsync(userId);
                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> fetchAllTaskItemUser()
        {
            try
            {
                var responce = await taskItemService.GetAllTaskItems();
                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateTimesheet([FromBody]Timesheet timesheet)
        {
            try
            {
                var responce = await timesheetService.InsertOrUpdateTimesheet(timesheet);

                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> TimesheetStatusChangeProcess([FromBody] TimesheetStatusChange timesheet)
        {
            try
            {
                var responce = await timesheetService.TimesheetStatusChangeProcess(timesheet);

                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> FetchUserPaindingAndApprovedHrs(long userId)
        {
            try
            {
                var responce = await timesheetService.FetchUserPaindingAndApprovedHrs(userId);
                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }

    }
}
