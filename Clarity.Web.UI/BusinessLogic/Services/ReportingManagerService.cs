using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class ReportingManagerService : IReportingManagerService
    {
        private readonly HttpClient _httpClient;

        public ReportingManagerService( HttpClientService httpClientService)
        {
            _httpClient = httpClientService.GetHttpClient();
        }

        public async Task<bool> CreateReportingManager(ReportingManager manager)
        {
            var repomanager = JsonConvert.SerializeObject(manager);
            var requestContent = new StringContent(repomanager,Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync("ReportingManager/InsertReportingManagerAsync", requestContent);
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<bool>(content);
                return responseContent != null ? responseContent : false;
            }
            return false;
        }

        public async Task<List<ReportingManagerVM>> FetchAllEmployeesByReportingManager(long managerId)
        {
            var urlContent = Path.Combine("ReportingManager/FetchAllEmployeesByReportingManager", managerId.ToString());
            var response = await _httpClient.GetAsync(urlContent);
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var contentResponse = JsonConvert.DeserializeObject<List<ReportingManagerVM>>(content);
                return contentResponse != null ? contentResponse : new List<ReportingManagerVM>();
            }
            return new List<ReportingManagerVM>();

        }

        public async Task<List<ReportingManagerVM>> FetchAllReportingManager()
        {
            var response = await _httpClient.GetAsync("ReportingManager/fetchAllReportingManagerAsync");
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var contentResponse = JsonConvert.DeserializeObject<List<ReportingManagerVM>>(content); 
                return contentResponse != null ? contentResponse : new List<ReportingManagerVM>();
            }

            return new List<ReportingManagerVM>();
        }

        public async Task<ReportingManagerVM> FetchReportingManager(long employeeId)
        {
            var urlcontent = Path.Combine("ReportingManager/FetchReportingManager", employeeId.ToString());
            var response = await _httpClient.GetAsync(urlcontent);
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var contentResponse = JsonConvert.DeserializeObject<ReportingManagerVM>(content);
                return contentResponse != null ? contentResponse : new ReportingManagerVM();
            }
            return new ReportingManagerVM();
        }

        public async Task<bool> UpdateReportingManager(long employeeId, ReportingManager reportingManager)
        {
            var repoManager = JsonConvert.SerializeObject(reportingManager);
            var requestContent = new StringContent(repoManager,Encoding.UTF8,"application/json");
            var uri = Path.Combine("ReportingManager", employeeId.ToString());
            var response = await _httpClient.PutAsync(uri, requestContent);
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responceContent = JsonConvert.DeserializeObject<bool>(content);
                return responceContent ? responceContent : false;
            }
            return false;
        }
    }
}
