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
        private readonly HttpClient _httpClient;

        public EmployeeSalaryService(HttpClientService httpClientService)
        {
            _httpClient = httpClientService.GetHttpClient();
        }
        public async Task<List<EmployeeSalaryModel>> FetchEmployeeSalaryAsync(long employeeId)
        {
            var url = "EmployeeSalary/FetchEmployeeSalaryAsync";

            var serviceUrl = Path.Combine(url, employeeId.ToString());
          
            var responce = await _httpClient.GetAsync(serviceUrl);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<EmployeeSalaryModel>>(content);
                return contentReqponce != null ? contentReqponce : new List<EmployeeSalaryModel>();
            }
            return new List<EmployeeSalaryModel>();
        }
        public async Task<List<EmployeeSalaryModel>> FetchAllEmployeeSalaries(string all = null, string employeeCode = null, string month = null, string year = null, long employeeId = 0)
        {
            var url = "EmployeeSalary/FetchAllEmployeeSalaries";
            var parameters = new List<string> { all, employeeCode, month, year, employeeId.ToString() };
            var filteredParameters = parameters.Where(p => p != null).ToList();
            if (filteredParameters.Any())
            {
                url += "/" + string.Join("/", filteredParameters);
            }
            var responce = await _httpClient.GetAsync(url);

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
