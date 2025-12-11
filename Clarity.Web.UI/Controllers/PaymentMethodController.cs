
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    public class PaymentMethodController : Controller
    {
        private readonly IPaymentMethodService _paymentMethodService;
        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
                this._paymentMethodService = paymentMethodService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPaymentMethods()
        {
            try
            {
                var responce = await _paymentMethodService.GetAllPaymentMethods();
                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
