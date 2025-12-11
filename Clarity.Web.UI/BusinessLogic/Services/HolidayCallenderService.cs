using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Newtonsoft.Json;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class HolidayCallenderService : IHolidayCallenderService
    {
        private readonly HttpClient _httpClient;

        public HolidayCallenderService(HttpClientService httpClientService)
        {
            _httpClient = httpClientService.GetHttpClient();
        }
        public async Task<List<HolidayCallender>> fetchAllHolidayCallendersAsync()
        {
            var responce = await _httpClient.GetAsync("HolidayCallender/fetchAllHolidayCallendersAsync");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<HolidayCallender>>(content);
                return contentReqponce != null ? contentReqponce : new List<HolidayCallender>();
            }
            return new List<HolidayCallender>();
        }

        public async Task<List<HolidayCallender>> fetchAllHolidayCallendersAsync(int year)
        {
            var urlContent = Path.Combine("HolidayCallender", year.ToString());
            var responce = await _httpClient.GetAsync(urlContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<HolidayCallender>>(content);
                return contentReqponce != null ? contentReqponce : new List<HolidayCallender>();
            }
            return new List<HolidayCallender>();
        }

        public async Task<bool> InsertOrUpdateHolidayCallenderAsync(HolidayCallender holidayCallender)
        {
            var inputContent = JsonConvert.SerializeObject(holidayCallender);

            var requestContent = new StringContent(inputContent, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("HolidayCallender/InsertOrUpdateHolidayCallenderAsync", requestContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var responceContent = JsonConvert.DeserializeObject<bool>(content);
                return responceContent ? responceContent : false;
            }
            return false;
        }

        public async Task<bool> RemoveHolidayCallenderAsync(long callenderId)
        {
            var urlContent = Path.Combine("HolidayCallender", callenderId.ToString());
            var responce = await _httpClient.DeleteAsync(urlContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<bool>(content);
                return contentReqponce ? contentReqponce : false;
            }
            return false;
        }
    }
}
