using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient _httpClient = null;

        private readonly CoreConfig coreConfig;

        public CountryService(IOptions<CoreConfig> _coreConfig, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthorizedClient");
            coreConfig = _coreConfig.Value;
            _httpClient.BaseAddress = new Uri(coreConfig.BaseUrl);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout.Add(new TimeSpan(0, 0, 60));

        }
        public async Task<List<Country>> fetchAllCountries()
        {
            var responce = await _httpClient.GetAsync("Country/fetchAllCountries");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<Country>>(content);
                return contentReqponce != null ? contentReqponce : new List<Country>();
            }
            return new List<Country>();
        }

        public async Task<bool> InsertOrUpdateCountry(Country country)
        {
            var inputContent = JsonConvert.SerializeObject(country);

            var requestContent = new StringContent(inputContent, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("Country/InsertOrUpdateCountry", requestContent);

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
