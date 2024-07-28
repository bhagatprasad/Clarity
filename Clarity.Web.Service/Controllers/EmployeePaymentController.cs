using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeePaymentController : ControllerBase
    {
        private readonly IEmployeePaymentService _employeePaymentService;
        private readonly ITutionFeeService _tutionFeeService;

        public EmployeePaymentController(IEmployeePaymentService employeePayment, ITutionFeeService tutionFeeService)
        {
            this._employeePaymentService = employeePayment;
            _tutionFeeService = tutionFeeService;

        }

        [HttpGet]
        [Route("GetAllEmployeePayments")]
        public async Task<IActionResult> GetAllEmployeePayments()
        {
            try
            {
                var data = await _employeePaymentService.GetAllEmployeePayments();
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("InsertEmployeePaymentAsync")]
        public async Task<IActionResult> InsertEmployeePaymentAsync(EmployeePayment employeePayment)
        {
            var data = await _employeePaymentService.InsertEmployeePayments(employeePayment);
            return Ok(data);
        }

    }
}
