using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Newtonsoft.Json;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class EmployeePaymentService: IEmployeePaymentService
    {
        private readonly HttpClient _httpClient;
        public EmployeePaymentService(HttpClientService httpClientService)
        {
            this._httpClient = httpClientService.GetHttpClient();
        }

        public async Task<List<EmployeePaymentModel>> GetAllEmployeePayments()
        {
            var responce = await _httpClient.GetAsync("EmployeePayment/GetAllEmployeePayments");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<EmployeePaymentModel>>(content);
                return contentReqponce != null ? contentReqponce : new List<EmployeePaymentModel>();
            }
            return new List<EmployeePaymentModel>();
        }

        public async Task<bool> InsertEmployeePayments(EmployeePayment employeePayment)
        {
            var content = new StringContent(JsonConvert.SerializeObject(employeePayment), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("EmployeePayment/InsertEmployeePaymentAsync", content);

            if (response.IsSuccessStatusCode)
            {
                return true; // Assuming successful insertion
            }

            return false;
        }
    }
}
