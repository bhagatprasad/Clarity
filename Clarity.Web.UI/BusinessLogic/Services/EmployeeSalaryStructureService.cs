using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Newtonsoft.Json;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class EmployeeSalaryStructureService : IEmployeeSalaryStructureService
    {
        private readonly HttpClient _httpClient;

        public EmployeeSalaryStructureService(HttpClientService httpClientService)
        {
            _httpClient = httpClientService.GetHttpClient();
        }
        public async Task<EmployeeSalaryStructure> fetchEmployeeSalaryStructure(long employeeId)
        {
            var urlcontent = Path.Combine("EmployeeSalaryStructure/fetchEmployeeSalaryStructuresAsync", employeeId.ToString());
            var responce = await _httpClient.GetAsync(urlcontent);
            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<EmployeeSalaryStructure>(content);
                return contentReqponce != null ? contentReqponce : new EmployeeSalaryStructure();
            }
            return new EmployeeSalaryStructure();
        }

        public async Task<List<EmployeeSalaryStructure>> fetchEmployeeSalaryStructuresAsync()
        {
            var responce = await _httpClient.GetAsync("EmployeeSalaryStructure/fetchEmployeeSalaryStructuresAsync");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<EmployeeSalaryStructure>>(content);
                return contentReqponce != null ? contentReqponce : new List<EmployeeSalaryStructure>();
            }
            return new List<EmployeeSalaryStructure>();
        }
    }
}
