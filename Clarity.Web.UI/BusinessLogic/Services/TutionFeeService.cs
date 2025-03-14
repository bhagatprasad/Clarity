using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class TutionFeeService : ITutionFeeService
    {
        private readonly HttpClient _httpClient;
        public TutionFeeService(HttpClientService httpClientService)
        {
            this._httpClient = httpClientService.GetHttpClient();
        }

        public async Task<List<EmployeeTutionFeesModel>> fetchAllTutionFees()
        {
            var responce = await _httpClient.GetAsync("TutionFee/fetchAllTutionFees");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<EmployeeTutionFeesModel>>(content);
                return contentReqponce != null ? contentReqponce : new List<EmployeeTutionFeesModel>();
            }
            return new List<EmployeeTutionFeesModel>();
        }

        public async Task<bool> InsertOrUpdateTutionFee(TutionFee tutionFee)
        {
            var inputContent = JsonConvert.SerializeObject(tutionFee);
            var requestContent = new StringContent(inputContent, Encoding.UTF8, "application/json");
            var responce = await _httpClient.PostAsync("TutionFee/InsertOrUpdateTutionFee", requestContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var mail = JsonConvert.DeserializeObject<bool>(content);
                return true;
            }
            return false;

        }
    }
}
