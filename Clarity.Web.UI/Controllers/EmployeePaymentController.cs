using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    public class EmployeePaymentController : Controller
    {
        private readonly IEmployeePaymentService _employeePaymentService;
        public EmployeePaymentController(IEmployeePaymentService employeePaymentService)
        {
            _employeePaymentService = employeePaymentService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployeePayments()
        {
            try
            {
                var responce = await _employeePaymentService.GetAllEmployeePayments();
                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertEmployeePayments([FromBody] EmployeePayment employeePayment)
        {
            try
            {
                var respnonce = await _employeePaymentService.InsertEmployeePayments(employeePayment);

                return Json(new { data = respnonce });
            }
            catch (Exception ex)
            {
                throw;
            }


        }
    }
}
