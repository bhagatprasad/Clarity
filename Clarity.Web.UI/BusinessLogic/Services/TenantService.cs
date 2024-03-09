using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class TenantService : ITenantService
    {
        private readonly HttpClient _httpClient;

        public TenantService(HttpClientService httpClientService)
        {
            _httpClient = httpClientService.GetHttpClient();
        }

        public async Task<List<User>> fetchUsers()
        {
            var responce = await _httpClient.GetAsync("Tenant/fetchUsers");
            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<User>>(content);
                return contentReqponce != null ? contentReqponce : new List<User>();
            }
            return new List<User>();
        }

        public async Task<bool> fnRegisterUserAsync(RegisterUser registerUser)
        {
            var inputContent = JsonConvert.SerializeObject(registerUser);

            var requestContent = new StringContent(inputContent, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("Tenant/RegisterUser", requestContent);

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
