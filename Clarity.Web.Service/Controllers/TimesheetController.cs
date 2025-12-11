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
    public class TimesheetController : ControllerBase
    {
        private readonly ITimesheetService timesheetService;
        public TimesheetController(ITimesheetService timesheetService)
        {
            this.timesheetService = timesheetService;
        }
        [HttpPost]
        [Route("InsertOrUpdateTimesheet")]
        public async Task<IActionResult> InsertOrUpdateTimesheet(Timesheet timesheet)
        {
            try
            {
                var result = await timesheetService.InsertOrUpdateTimesheet(timesheet);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("TimesheetStatusChangeProcess")]
        public async Task<IActionResult> TimesheetStatusChangeProcess(TimesheetStatusChange timesheetStatusChange)
        {
            try
            {
                var result = await timesheetService.TimesheetStatusChangeProcess(timesheetStatusChange);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("FetchAllTimesheetsAsync")]
        public async Task<IActionResult> FetchAllTimesheetsAsync()
        {
            try
            {
                var documents = await timesheetService.GetAllTimesheetsAsync();
                return Ok(documents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("FetchAllTimesheetsAsync/{userId}")]
        public async Task<IActionResult> FetchAllTimesheetsAsync(long userId)
        {
            try
            {
                var timesheets = await timesheetService.GetAllTimesheetsAsync(userId);
                return Ok(timesheets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("FetchAllTimesheetsByStatusAsync/{status}")]
        public async Task<IActionResult> FetchAllTimesheetsByStatusAsync(string status)
        {
            try
            {
                var timesheets = await timesheetService.GetAllTimesheetsAsync(status);
                return Ok(timesheets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("FetchAllTimesheetsUserAsync/{userId}/{status}")]
        public async Task<IActionResult> FetchAllTimesheetsUserAsync(long userId, string status)
        {
            try
            {
                var timesheets = await timesheetService.GetAllTimesheetsAsync(userId, status);
                return Ok(timesheets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("FetchAllTimesheetsByDatesAsync/{fromDate}/{toDate}")]
        public async Task<IActionResult> FetchAllTimesheetsByDatesAsync(DateTimeOffset? fromDate, DateTimeOffset? toDate)
        {
            try
            {
                var timesheets = await timesheetService.GetAllTimesheetsAsync(fromDate, toDate);
                return Ok(timesheets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("FetchAllTimesheetsByUserDatesAsync/{userId}/{fromDate}/{toDate}")]
        public async Task<IActionResult> FetchAllTimesheetsByUserDatesAsync(long userId,DateTimeOffset? fromDate, DateTimeOffset? toDate)
        {
            try
            {
                var timesheets = await timesheetService.GetAllTimesheetsAsync(fromDate, toDate);
                return Ok(timesheets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("FetchUserPaindingAndApprovedHrs/{userId}")]
        public async Task<IActionResult> FetchUserPaindingAndApprovedHrs(long userId)
        {
            try
            {
                var response = await timesheetService.FetchUserPaindingAndApprovedHrs(userId);
                return Ok(response);
            }
            catch(Exception ex) 
            {
               return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
