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
    public class ReportingManagerController : ControllerBase
    {
        private readonly IReportingManager reportingManager;
        public ReportingManagerController(IReportingManager _reportingManager)
        {
            this.reportingManager = _reportingManager;
        }
        [HttpGet]
        [Route("fetchAllReportingManagerAsync")]
        public async Task<ActionResult<List<ReportingManagerVM>>> fetchAllReportingManagerAsync()
        {
            try
            {
                var manager = await reportingManager.FetchAllReportingManager();
                return manager;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching reporting manager: {ex.Message}");
            }
        }

        [HttpGet("fetchReportingManagerAsync/{employeeId}")]
        public async Task<ActionResult<ReportingManagerVM>> fetchReportingManagerAsync(long employeeId)
        {
            try
            {
                var employee = await reportingManager.FetchReportingManager(employeeId);
                if (employee == null)
                {
                    return NotFound();
                }
                return employee;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching employee with ID {employeeId}: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("InsertReportingManagerAsync")]
        public async Task<ActionResult<bool>> InsertReportingManagerAsync(RepotingManager repoManager)
        {
            try
            {
                var result = await reportingManager.CreateReportingManager(repoManager);
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while inserting/updating reporting manager: {ex.Message}");
            }
        }

        [Route("{employeeId:long}")]
        [HttpPut]
        public async Task<IActionResult> UpdateReportingManager(long employeeId, RepotingManager repoManager)
        {
            try
            {
                await reportingManager.UpdateReportingManager(employeeId, repoManager);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Updating ReportingManager", 500, ex.Message);
            }
        }
    }
}
