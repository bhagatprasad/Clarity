using AspNetCoreHero.ToastNotification.Notyf;

using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Clarity.Web.UI.Controllers
{
    public class TutionFeeController : Controller
    {
        private readonly ITutionFeeService _tutionFeeService;

        public TutionFeeController(ITutionFeeService tutionFeeService)
        {
            this._tutionFeeService = tutionFeeService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> fetchAllTutionFees()
        {
            try
            {
                var responce = await _tutionFeeService.fetchAllTutionFees();
                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateTutionFee([FromBody] TutionFee tutionFee)
        {
            try
            {
                var respnonce = await _tutionFeeService.InsertOrUpdateTutionFee(tutionFee);

                return Json(new { data = respnonce });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
