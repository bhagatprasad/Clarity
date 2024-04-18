using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
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
    }
}
