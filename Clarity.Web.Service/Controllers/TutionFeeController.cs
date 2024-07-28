using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutionFeeController : ControllerBase
    {
        private readonly ITutionFeeService _tutionFeeService;
        public TutionFeeController(ITutionFeeService tutionFeeService)
        {
            this._tutionFeeService = tutionFeeService;    
        }

        [HttpPost]
        [Route("InsertOrUpdateTutionFee")]
        public async Task<IActionResult> InsertOrUpdateTutionFee(TutionFee tutionFee)
        {
            try
            {
                var result = await _tutionFeeService.InsertOrUpdateTutionFee(tutionFee);
                if (result)
                    return Ok("City inserted or updated successfully.");
                else
                    return BadRequest("Failed to insert or update city.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while inserting or updating tutionFee: {ex.Message}");
            }
        }

            [HttpGet]
            [Route("fetchAllTutionFees")]
            public async Task<IActionResult> fetchAllTutionFees()
            {
                try
                {
                    var cities = await _tutionFeeService.fetchAllTutionFees();
                    return Ok(cities);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while fetching AllTutionFees: {ex.Message}");
                   
                }
            }
        
    }
}
