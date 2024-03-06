using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Microsoft.Extensions.Options;
using Microsoft.TeamFoundation.Common;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient = null;

        private readonly CoreConfig coreConfig;

        public EmployeeService(IOptions<CoreConfig> _coreConfig, IHttpClientFactory httpClientFactory)
        {

            _httpClient = httpClientFactory.CreateClient("AuthorizedClient");
            coreConfig = _coreConfig.Value;
            _httpClient.BaseAddress = new Uri(coreConfig.BaseUrl);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout.Add(new TimeSpan(0, 0, 60));

        }
        public async Task<List<AddEditEmployee>> fetchAllEmployeesAsync()
        {
            var responce = await _httpClient.GetAsync("Employee/fetchAllEmployeesAsync");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<AddEditEmployee>>(content);
                return contentReqponce != null ? contentReqponce : new List<AddEditEmployee>();
            }
            return new List<AddEditEmployee>();
        }

        public async Task<AddEditEmployee> fetchEmployeeAsync(long employeeId)
        {
            var urlcontent = Path.Combine("Employee/fetchEmployeeAsync", employeeId.ToString());
            var responce = await _httpClient.GetAsync(urlcontent);
            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<AddEditEmployee>(content);
                return contentReqponce != null ? contentReqponce : new AddEditEmployee();
            }
            return new AddEditEmployee();
        }

        public async Task<bool> InsertOrUpdateAsync(AddEditEmployee employee)
        {
            var inputContent = JsonConvert.SerializeObject(employee);

            var requestContent = new StringContent(inputContent, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("Employee/InsertOrUpdateEmployeeAsync", requestContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var responceContent = JsonConvert.DeserializeObject<bool>(content);
                return responceContent  ? responceContent : false;
            }
            return false;

        }

        //private AddEditEmployee ValidateEmployeeInput(AddEditEmployee employeeDetails)
        //{
        //    if (employeeDetails.employee.DateOfBirth == null)
        //    {
        //        // Parse the date string only if DateOfBirth is null
        //        employeeDetails.employee.DateOfBirth = DateTimeOffset.Parse(employeeDetails.employee.DateOfBirth, new CultureInfo("en-US", true));
        //    }

        //    return employeeDetails;
        //}
    }
}
