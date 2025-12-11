using Clarity.Web.UI.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    public class PaymentTypeController : Controller
    {
        private readonly IPaymentTypeService _paymentTypeService;
        public PaymentTypeController(IPaymentTypeService paymentTypeService)
        {
            _paymentTypeService = paymentTypeService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPaymentTypes()
        {
            try
            {
                var responce = await _paymentTypeService.GetAllPaymentTypes();
                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
