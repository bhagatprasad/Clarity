using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class AuthenticationService : IClarityAuthenticationService
    {
        private readonly HttpClient _httpClient = null;

        private readonly CoreConfig coreConfig;

        public AuthenticationService(IOptions<CoreConfig> _coreConfig)
        {
            _httpClient = new HttpClient();
            coreConfig = _coreConfig.Value;
            _httpClient.BaseAddress = new Uri(coreConfig.BaseUrl);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout.Add(new TimeSpan(0, 0, 60));
        }
        public async Task<AuthResponse> Authenticateuser(Authenticateuser authenticateuser)
        {
            var inputContent = JsonConvert.SerializeObject(authenticateuser);

            var requestContent = new StringContent(inputContent, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("Auth/AuthenticateUser", requestContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var responceContent = JsonConvert.DeserializeObject<AuthResponse>(content);
                return responceContent != null ? responceContent : null;
            }
            return null;
        }

        public async Task<ApplicationUser> GenarateUserClaims(AuthResponse authResponse)
        {
            var inputContent = JsonConvert.SerializeObject(authResponse);

            var requestContent = new StringContent(inputContent, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("Auth/GenarateUserClaims", requestContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var responceContent = JsonConvert.DeserializeObject<ApplicationUser>(content);
                return responceContent != null ? responceContent : null;
            }
            return null;
        }
    }
}
