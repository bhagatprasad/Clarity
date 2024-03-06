using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class MonthlySalaryService : IMonthlySalaryService
    {
        private readonly HttpClient _httpClient = null;

        private readonly CoreConfig coreConfig;

        public MonthlySalaryService(IOptions<CoreConfig> _coreConfig, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthorizedClient");
            coreConfig = _coreConfig.Value;
            _httpClient.BaseAddress = new Uri(coreConfig.BaseUrl);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout.Add(new TimeSpan(0, 0, 60));

        }
        public async Task<List<MonthlySalary>> fetchAllMonthlySalaries()
        {
            var responce = await _httpClient.GetAsync("MonthlySalary/FetchAllMonthlySalaries");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<MonthlySalary>>(content);
                return contentReqponce != null ? contentReqponce : new List<MonthlySalary>();
            }
            return new List<MonthlySalary>();
        }

        public async Task<bool> PublishMonthlySalary(MonthlySalary monthlySalary)
        {
            var inputContent = JsonConvert.SerializeObject(monthlySalary);

            var requestContent = new StringContent(inputContent, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("MonthlySalary/PublishMonthlySalary", requestContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();

                var responceContent = JsonConvert.DeserializeObject<bool>(content);

                return responceContent ? responceContent : false;
            }
            return false;
        }
    }
}
