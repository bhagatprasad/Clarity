using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Software Engineer/Developer,Full-stack Developer")]
    public class UserTimesheetController : Controller
    {
        private readonly ITimesheetService timesheetService;
        private readonly INotyfService notyfService;
        public UserTimesheetController(ITimesheetService timesheetService,
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
        public async Task<IActionResult> fetchAllUserTimesheets(long userId)
        {
            try
            {
                var responce = await timesheetService.GetAllTimesheetsAsync(userId);
                return Json(new { data = responce });
            }catch(Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
