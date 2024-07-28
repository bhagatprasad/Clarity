using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentTypeService _paymentTypeService;
        public PaymentTypeController(IPaymentTypeService paymentMethodService)
        {
            this._paymentTypeService = paymentMethodService;   
        }
        [HttpPost]
        [Route("InsertPaymentTypeAsync")]
        public async Task<ActionResult> InsertPaymentTypeAsync(PaymentType paymentType)
        {
            try
            {
                var responce = await _paymentTypeService.InsertPaymentTypes(paymentType);

                return Ok(responce);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpGet]
        [Route("GetAllPaymentTypes")]
        public async Task<IActionResult> GetAllPaymentTypes()
        {
            try
            {
                var countries = await _paymentTypeService.GetAllPaymentTypes();
                return Ok(countries);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
