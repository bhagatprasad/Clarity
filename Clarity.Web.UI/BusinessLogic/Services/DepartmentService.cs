using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Microsoft.Extensions.Options;
using Microsoft.TeamFoundation.Common;
using Newtonsoft.Json;
using Clarity.Web.Service.Helpers;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class DepartmentService :IDepartmentService
    {
        private readonly HttpClient httpClient = null;
        private readonly CoreConfig coreConfig;

        public DepartmentService(IOptions<CoreConfig> _coreConfig )
        {
            httpClient = new HttpClient();
            this.coreConfig = _coreConfig.Value;
            httpClient.BaseAddress = new Uri(coreConfig.BaseUrl);
            httpClient.Timeout = new TimeSpan(0,0,30);
            httpClient.DefaultRequestHeaders.Clear();
        }

        public async Task<bool> CreateDepartment(Department department)
        {
            var _department = JsonConvert.SerializeObject(department);
            var requestContent = new StringContent(_department,Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Department/CreateDepartment", requestContent);
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<bool>(content);
                return responseContent != null ? responseContent : false;

            }
            return false;

        }

        public async Task<bool> DeleteDepartment(long departmentId)
        {
            var uri = Path.Combine("Department", departmentId.ToString());
            var response = await httpClient.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var responceContent = JsonConvert.DeserializeObject<HttpRequestResponseMessage<bool>>(content);

                return responceContent != null ? responceContent.Data : false;
            }
            return false;
        }

        public async Task<List<Department>> GetAllDepartment()
        {
            var departmentList = new List<Department>();

            var response = await httpClient.GetAsync("Department");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var responceContent = JsonConvert.DeserializeObject<HttpRequestResponseMessage<List<Department>>>(content);

                return responceContent != null ? responceContent.Data : departmentList;
            }
            return departmentList;
        }

        public async Task<Department> GetDepartment(long departmentId)
        {
            var uri = Path.Combine("Department", departmentId.ToString());
            var response = await httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responceContent = JsonConvert.DeserializeObject<HttpRequestResponseMessage<Department>>(content);
                return responceContent != null ? responceContent.Data : new Department();
            }
            return new Department();
        }

        public async Task<bool> UpdateDepartment(long departmentId, Department department)
        {
            var _department = JsonConvert.SerializeObject(department);
            var requestContent = new StringContent(_department, Encoding.UTF8, "application/json");
            var uri = Path.Combine("Department", departmentId.ToString());
            var responce = await httpClient.PutAsync(uri, requestContent);
            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var responceContent = JsonConvert.DeserializeObject<HttpRequestResponseMessage<bool>>(content);
                return responceContent != null ? responceContent.Data : false;
            }
            return false;
        }

        public async Task<bool> VerifyDepartmentAlreadyExists(string department)
        {
            var uri = Path.Combine("Department", department.ToString());
            var response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(content);
        }
    }
}
