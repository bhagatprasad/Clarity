using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Clarity.Web.Service.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeSalaryController : ControllerBase
    {
        private readonly IEmployeeSalaryService _employeeSalaryService;
        public EmployeeSalaryController(IEmployeeSalaryService employeeSalaryService)
        {
            this._employeeSalaryService = employeeSalaryService;
        }

        [HttpGet]
        [Route("FetchAllEmployeeSalaries/{all?}/{employeeCode?}/{month?}/{year?}/{employeeId?}")]
        public async Task<ActionResult<List<EmployeeSalaryModel>>> FetchAllEmployeeSalaries(string all = "", string employeeCode = "", string month = "",
                                                                                string year = "", long employeeId = 0)
        {
            
            try
            {
                List<EmployeeSalaryModel> employeeSalaries = new List<EmployeeSalaryModel>();
                if (!string.IsNullOrEmpty(all))
                {
                    employeeSalaries = await _employeeSalaryService.FetchAllEmployeeSalarys();
                }
                else if (!string.IsNullOrEmpty(employeeCode))
                {
                    employeeSalaries = await _employeeSalaryService.FetchAllEmployeeSalarys(employeeCode);
                }
                else if (!string.IsNullOrEmpty(month) || !string.IsNullOrEmpty(year))
                {
                    employeeSalaries = await _employeeSalaryService.FetchAllEmployeeSalarys(month, year);
                }
                else if (employeeId != 0)
                {
                    employeeSalaries = await _employeeSalaryService.FetchAllEmployeeSalarys(employeeId);
                }
                return employeeSalaries;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
        [HttpGet]
        [Route("FetchEmployeeSalary/{employeeSalaryId?}")]
        public async Task<EmployeeSalaryModel> FetchEmployeeSalary (long employeeSalaryId)
        {
            try
            {
                var employeeSalary = await _employeeSalaryService.FetchEmployeeSalary(employeeSalaryId);
                 return employeeSalary;
            }
            catch (Exception ex)
            {
                throw new Exception (ex.Message, ex);
            }
        }
    }
}
