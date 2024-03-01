using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class CityService : ICityService
    {
        private readonly HttpClient _httpClient = null;

        private readonly CoreConfig coreConfig;

        public CityService(IOptions<CoreConfig> _coreConfig, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthorizedClient");
            coreConfig = _coreConfig.Value;
            _httpClient.BaseAddress = new Uri(coreConfig.BaseUrl);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout.Add(new TimeSpan(0, 0, 60));
        }
        public async Task<List<City>> fetchAllCities()
        {
            var responce = await _httpClient.GetAsync("City/fetchAllCities");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<City>>(content);
                return contentReqponce != null ? contentReqponce : new List<City>();
            }
            return new List<City>();
        }

        public async Task<bool> InsertOrUpdateCity(City city)
        {
            var inputContent = JsonConvert.SerializeObject(city);

            var requestContent = new StringContent(inputContent, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("City/InsertOrUpdateCity", requestContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var responceContent = JsonConvert.DeserializeObject<bool>(content);
                return responceContent != null ? responceContent : false;
            }
            return false;
        }
    }
}
