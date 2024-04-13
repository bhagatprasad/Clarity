using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Admin")]
    public class HolidayCallenderController : Controller
    {
        private readonly IHolidayCallenderService holidayCallenderService;
        private readonly INotyfService notyfService;
        public HolidayCallenderController(IHolidayCallenderService holidayCallenderService,
            INotyfService notyfService)
        {
            this.holidayCallenderService = holidayCallenderService;
            this.notyfService = notyfService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> fetchAllHolidayCallenders()
        {
            try
            {
                var responce = await holidayCallenderService.fetchAllHolidayCallendersAsync();
                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateHolyDay([FromBody]HolidayCallender holidayCallender)
        {
            try
            {
                var responce = await holidayCallenderService.InsertOrUpdateHolidayCallenderAsync(holidayCallender);
                notyfService.Success("Holyday was created");
                return Json(responce);
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
