using Clarity.Web.Service.Helpers;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Clarity.Web.Service.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ClarityAuthorize]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService countryService;

        public CountryController(ICountryService _countryService)
        {
            this.countryService = _countryService;
        }

        [HttpGet]
        [Route("fetchAllCountries")]
        public async Task<IActionResult> fetchAllCountries()
        {
            try
            {
                var countries = await countryService.fetchAllCountries();
                return Ok(countries);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("InsertOrUpdateCountry")]
        public async Task<ActionResult> InsertOrUpdateCountry(Country country)
        {
            try
            {
                var responce = await countryService.InsertOrUpdateCountry(country);

                return Ok(responce);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
