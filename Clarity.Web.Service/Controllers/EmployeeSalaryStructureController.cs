using Clarity.Web.Service.Helpers;
using Clarity.Web.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ClarityAuthorize]
    public class EmployeeSalaryStructureController : ControllerBase
    {
        private readonly IEmployeeSalaryStructureService employeeSalaryStructureService;
        public EmployeeSalaryStructureController(IEmployeeSalaryStructureService employeeSalaryStructureService)
        {
            this.employeeSalaryStructureService = employeeSalaryStructureService;
        }

        [HttpGet]
        [Route("fetchEmployeeSalaryStructuresAsync")]
        public async Task<IActionResult> fetchEmployeeSalaryStructuresAsync()
        {
            try
            {
                var responce = await employeeSalaryStructureService.fetchEmployeeSalaryStructuresAsync();

                return Ok(responce);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("fetchEmployeeSalaryStructuresAsync/{employeeId}")]
        public async Task<IActionResult> fetchEmployeeSalaryStructuresAsync(long employeeId)
        {
            try
            {
                var responce = await employeeSalaryStructureService.fetchEmployeeSalaryStructure(employeeId);

                return Ok(responce);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
