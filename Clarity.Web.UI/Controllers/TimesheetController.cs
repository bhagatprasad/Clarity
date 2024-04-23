using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Admin")]
    public class TimesheetController : Controller
    {
        private readonly ITimesheetService timesheetService;
        private readonly INotyfService notyfService;
        public TimesheetController(ITimesheetService timesheetService,
            INotyfService notyfService)
        {
            this.notyfService = notyfService;
            this.timesheetService = timesheetService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> fetchAllTimesheets()
        {
            try
            {
                var responce = await timesheetService.GetAllTimesheetsAsync();

                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }

        }



        [HttpPost]
        public async Task<IActionResult> SaveInsertOrUpdateTimesheet([FromBody] Timesheet timesheet)
        {

            if (timesheet != null)
            {
                bool responce = false;

                responce = await timesheetService.InsertOrUpdateTimesheet(timesheet);

                if (responce)
                {
                    notyfService.Success("TimeSheet was Created successfully");

                    return Json(true);
                }
                notyfService.Error("Somwthing went wrong");
                return Json(responce);
            }

            notyfService.Error("Somwthing went wrong");

            return Json(false);
        }



    }
}
