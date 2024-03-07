using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class EmployeeSalaryService : IEmployeeSalaryService
    {
        private readonly HttpClient _httpClient = null;
        private readonly CoreConfig coreConfig;

        public EmployeeSalaryService(IOptions<CoreConfig> _coreConfig, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthorizeClient");
            coreConfig = _coreConfig.Value;
            _httpClient.BaseAddress = new Uri(coreConfig.BaseUrl);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout.Add(new TimeSpan(0, 0, 60));
        }
        public async Task<List<EmployeeSalaryModel>> FetchAllEmployeeSalaries(string all = "", string employeeCode = null, string month = "", string year = "", long employeeId = 0)
        {
            var responce = await _httpClient.GetAsync("EmployeeSalary/AllFetchAllEmployeeSalaries");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<EmployeeSalaryModel>>(content);
                return contentReqponce != null ? contentReqponce : new List<EmployeeSalaryModel>();
            }
            return new List<EmployeeSalaryModel>();
        }

        public async Task<EmployeeSalaryModel> FetchEmployeeSalary(long employeeSalaryId)
        {
            var urlcontent = Path.Combine("EmployeeSalary/FetchEmployeeSalary", employeeSalaryId.ToString());
            var responce = await _httpClient.GetAsync(urlcontent);
            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<EmployeeSalaryModel>(content);
                return contentReqponce != null ? contentReqponce : new EmployeeSalaryModel();
            }
            return new EmployeeSalaryModel();
        }
    }
}
