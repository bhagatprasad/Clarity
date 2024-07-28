using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;
        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
                this._paymentMethodService = paymentMethodService;
        }
        [HttpGet]
        [Route("GetAllPaymentMethods")]
        public async Task <IActionResult> GetAllPaymentMethods()
        {
           
            try
            {
                var data = await _paymentMethodService.GetAllPaymentMethods();
                return Ok(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("InsertPaymentMethodAync")]
        public async Task<IActionResult> InsertPaymentMethodAync(PaymentMethod paymentMethod)
        {
            var data = await _paymentMethodService.InsertPaymentMethods(paymentMethod);
            return Ok(data);
        }
    }
}
