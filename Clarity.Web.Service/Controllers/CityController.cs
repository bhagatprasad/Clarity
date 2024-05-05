using Clarity.Web.Service.Helpers;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ClarityAuthorize]
    public class CityController : ControllerBase
    {
        private readonly ICityService cityService;
        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
        }
        [HttpGet]
        [Route("fetchAllCities")]
        public async Task<IActionResult> fetchAllCities()
        {
            try
            {
                var cities = await cityService.fetchAllCities();
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while fetching cities: {ex.Message}");
                //return StatusCode(500, $"An error occurred while fetching cities: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateCity")]
        public async Task<IActionResult> InsertOrUpdateCity(City city)
        {
            try
            {
                var result = await cityService.InsertOrUpdateCity(city);
                if (result)
                    return Ok("City inserted or updated successfully.");
                else
                    return BadRequest("Failed to insert or update city.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while inserting or updating city: {ex.Message}");
            }
        }
    }
}
