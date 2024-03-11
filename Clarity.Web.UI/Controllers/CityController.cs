using AspNetCoreHero.ToastNotification.Notyf;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Admin")]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        public CityController(ICityService _cityService)
        {
            this._cityService = _cityService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> fetchAllCities()
        {
            try
            {
                var responce = await _cityService.fetchAllCities();
                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
