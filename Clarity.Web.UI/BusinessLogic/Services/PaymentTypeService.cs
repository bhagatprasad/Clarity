using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Newtonsoft.Json;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class PaymentTypeService: IPaymentTypeService
    {
        private readonly HttpClient _httpClient;
        public PaymentTypeService(HttpClientService httpClientService)
        {
            this._httpClient = httpClientService.GetHttpClient();
        }

        public async Task<List<PaymentType>> GetAllPaymentTypes()
        {
            var responce = await _httpClient.GetAsync("PaymentType/GetAllPaymentTypes");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<PaymentType>>(content);
                return contentReqponce != null ? contentReqponce : new List<PaymentType>();
            }
            return new List<PaymentType>();
        }
    }
}
