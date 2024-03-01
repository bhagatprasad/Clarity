using Clarity.Web.UI.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    [Authorize]
    public class CountryController : Controller
    {
        private readonly ICountryService countryService;
        public CountryController(ICountryService countryService) { 
            this.countryService = countryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> fetchAllCountries()
        {
            var responce = await countryService.fetchAllCountries();
            return Json(new { data = responce });
        }
    }
}
