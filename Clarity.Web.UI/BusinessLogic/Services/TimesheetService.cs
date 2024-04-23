using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Newtonsoft.Json;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class TimesheetService : ITimesheetService
    {
        private readonly HttpClient _httpClient;

        public TimesheetService(HttpClientService httpClientService)
        {
            _httpClient = httpClientService.GetHttpClient();
        }

        public async Task<List<Timesheet>> GetAllTimesheetsAsync()
        {
            var responce = await _httpClient.GetAsync("Timesheet/FetchAllTimesheetsAsync");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<Timesheet>>(content);
                return contentReqponce != null ? contentReqponce : new List<Timesheet>();
            }
            return new List<Timesheet>();
        }

        public async Task<List<Timesheet>> GetAllTimesheetsAsync(long userId)
        {
            var contentUrl = Path.Combine("Timesheet/FetchAllTimesheetsAsync", userId.ToString());
            var responce = await _httpClient.GetAsync(contentUrl);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<Timesheet>>(content);
                return contentReqponce != null ? contentReqponce : new List<Timesheet>();
            }
            return new List<Timesheet>();
        }

        public async Task<List<Timesheet>> GetAllTimesheetsAsync(string status)
        {
            var contentUrl = Path.Combine("Timesheet/FetchAllTimesheetsByStatusAsync", status.ToString());
            var responce = await _httpClient.GetAsync(contentUrl);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<Timesheet>>(content);
                return contentReqponce != null ? contentReqponce : new List<Timesheet>();
            }
            return new List<Timesheet>();
        }

        public async Task<List<Timesheet>> GetAllTimesheetsAsync(long userId, string status)
        {
            var contentUrl = Path.Combine("Timesheet/FetchAllTimesheetsUserAsync", userId.ToString() + "/" + status);
            var responce = await _httpClient.GetAsync(contentUrl);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<Timesheet>>(content);
                return contentReqponce != null ? contentReqponce : new List<Timesheet>();
            }
            return new List<Timesheet>();
        }

        public async Task<List<Timesheet>> GetAllTimesheetsAsync(DateTimeOffset? fromdate, DateTimeOffset? todate)
        {
            var contentUrl = Path.Combine("Timesheet/FetchAllTimesheetsByDatesAsync", fromdate.ToString() + "/" + todate.ToString());
            var responce = await _httpClient.GetAsync(contentUrl);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<Timesheet>>(content);
                return contentReqponce != null ? contentReqponce : new List<Timesheet>();
            }
            return new List<Timesheet>();
        }

        public async Task<List<Timesheet>> GetAllTimesheetsAsync(long userId, DateTimeOffset? fromdate, DateTimeOffset? todate)
        {
            var contentUrl = Path.Combine("Timesheet/FetchAllTimesheetsByUserDatesAsync", userId.ToString() + "/" + fromdate.ToString() + "/" + todate.ToString());
            var responce = await _httpClient.GetAsync(contentUrl);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<Timesheet>>(content);
                return contentReqponce != null ? contentReqponce : new List<Timesheet>();
            }
            return new List<Timesheet>();
        }

        public async Task<bool> InsertOrUpdateTimesheet(Timesheet timesheet)
        {
            
            var inputContent = JsonConvert.SerializeObject(timesheet);

            var requestContent = new StringContent(inputContent, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("Timesheet/InsertOrUpdateTimesheet", requestContent);
            
            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();

                var responceContent = JsonConvert.DeserializeObject<bool>(content);

                return responceContent ? responceContent : false;
            }
            return false;
        }

        public async Task<bool> TimesheetStatusChangeProcess(TimesheetStatusChange timesheetStatusChange)
        {
            var inputContent = JsonConvert.SerializeObject(timesheetStatusChange);

            var requestContent = new StringContent(inputContent, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("Timesheet/TimesheetStatusChangeProcess", requestContent);

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
