using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Clarity.Web.Service.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }
        [HttpGet]
        [Route("fetchAllEmployeesAsync")]
        public async Task<ActionResult<List<AddEditEmployee>>> fetchAllEmployeesAsync()
        {
            try
            {
                var employees = await _employeeService.fetchAllEmployeesAsync();
                return employees;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching employees: {ex.Message}");
            }
        }

        [HttpGet("fetchEmployeeAsync/{employeeId}")]
        public async Task<ActionResult<AddEditEmployee>> fetchEmployeeAsync(long employeeId)
        {
            try
            {
                var employee = await _employeeService.fetchEmployeeAsync(employeeId);
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
        [Route("InsertOrUpdateEmployeeAsync")]
        public async Task<ActionResult<bool>> InsertOrUpdateEmployeeAsync(AddEditEmployee employee)
        {
            try
            {
                var result = await _employeeService.InsertOrUpdateAsync(employee);
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while inserting/updating employee: {ex.Message}");
            }
        }
    }
}
