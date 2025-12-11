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
    public class HolidayCallenderController : ControllerBase
    {
        private readonly IHolidayCallenderService _holidayCallenderService;

        public HolidayCallenderController(IHolidayCallenderService holidayCallenderService)
        {
            _holidayCallenderService = holidayCallenderService;
        }

        [HttpGet]
        [Route("fetchAllHolidayCallendersAsync")]
        public async Task<ActionResult<List<HolidayCallender>>> GetAllHolidayCallendersAsync()
        {
            try
            {
                var holidayCallenders = await _holidayCallenderService.fetchAllHolidayCallendersAsync();
                return Ok(holidayCallenders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("fetchAllHolidayCallendersAsync/{year}")]
        public async Task<ActionResult<List<HolidayCallender>>> GetHolidayCallendersByYearAsync(int year)
        {
            try
            {
                var holidayCallenders = await _holidayCallenderService.fetchAllHolidayCallendersAsync(year);
                return Ok(holidayCallenders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateHolidayCallenderAsync")]
        public async Task<ActionResult> InsertOrUpdateHolidayCallenderAsync(HolidayCallender holidayCallender)
        {
            try
            {
                var result = await _holidayCallenderService.InsertOrUpdateHolidayCallenderAsync(holidayCallender);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{callenderId}")]
        public async Task<ActionResult> RemoveHolidayCallenderAsync(long callenderId)
        {
            try
            {
                var result = await _holidayCallenderService.RemoveHolidayCallenderAsync(callenderId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
