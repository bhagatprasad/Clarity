using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class DesignationService : IDesignationService
    {
        private readonly HttpClient _httpClient = null;

        private readonly CoreConfig coreConfig;

        public DesignationService(IOptions<CoreConfig> _coreConfig, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthorizedClient");
            coreConfig = _coreConfig.Value;
            _httpClient.BaseAddress = new Uri(coreConfig.BaseUrl);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout.Add(new TimeSpan(0, 0, 60));
        }
        public async Task<bool> CreateDesignation(Designation designation)
        {
            var inputContent = JsonConvert.SerializeObject(designation);

            var requestContent = new StringContent(inputContent, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("Designation/CreateDesignation", requestContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var responceContent = JsonConvert.DeserializeObject<bool>(content);
                return responceContent ? responceContent : false;
            }
            return false;
        }

        public async Task<List<Designation>> GetAllDesignation()
        {
            var responce = await _httpClient.GetAsync("Designation/GetAllDesignation");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<Designation>>(content);
                return contentReqponce != null ? contentReqponce : new List<Designation>();
            }
            return new List<Designation>();
        }
    }
}
