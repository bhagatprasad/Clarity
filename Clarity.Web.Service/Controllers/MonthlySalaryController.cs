using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Clarity.Web.Service.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonthlySalaryController : ControllerBase
    {
        private readonly IMonthlySalaryService monthlySalaryService;
        public MonthlySalaryController(IMonthlySalaryService monthlySalaryService)
        {
            this.monthlySalaryService = monthlySalaryService;
        }
        [HttpPost("PublishMonthlySalary")]
        public async Task<IActionResult> PublishMonthlySalary(MonthlySalary monthlySalary)
        {
            try
            {
                if (monthlySalary == null)
                {
                    return BadRequest("Monthly salary object is null.");
                }

                var responce = await monthlySalaryService.PublishMonthlySalary(monthlySalary);

                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("FetchAllMonthlySalaries")]
        public async Task<IActionResult> FetchAllMonthlySalaries()
        {
            try
            {
                var monthlySalaries = await monthlySalaryService.fetchAllMonthlySalaries();

                return Ok(monthlySalaries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
