using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Newtonsoft.Json;
using System.Net.Http;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class PaymentMethodService: IPaymentMethodService
    {
        private readonly HttpClient _httpClient;
        public PaymentMethodService(HttpClientService httpClientService)
        {
            this._httpClient = httpClientService.GetHttpClient();
        }

        public async Task<List<PaymentMethod>> GetAllPaymentMethods()
        {
            var responce = await _httpClient.GetAsync("PaymentMethod/GetAllPaymentMethods");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<PaymentMethod>>(content);
                return contentReqponce != null ? contentReqponce : new List<PaymentMethod>();
            }
            return new List<PaymentMethod>();
        }

    }
}
