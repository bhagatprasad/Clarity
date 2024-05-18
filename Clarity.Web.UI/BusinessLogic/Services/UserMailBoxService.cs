using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Newtonsoft.Json;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class UserMailBoxService : IUserMailBoxService
    {
        private readonly HttpClient _httpClient;
        public UserMailBoxService(HttpClientService httpClientService)
        {
            _httpClient = httpClientService.GetHttpClient();
        }
        public async Task<List<UserMailBox>> GetAllUserMailBoxAsync()
        {
            var responce = await _httpClient.GetAsync("UserMailBox/GetAllUserMailBoxAsync");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<UserMailBox>>(content);
                return contentReqponce != null ? contentReqponce : new List<UserMailBox>();
            }
            return new List<UserMailBox>();
        }

        public async Task<List<UserMailBox>> GetAllUserMailBoxAsync(long userId)
        {
            var responce = await _httpClient.GetAsync("UserMailBox/GetAllUserMailBoxByID/userId");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<UserMailBox>>(content);
                return contentReqponce != null ? contentReqponce : new List<UserMailBox>();
            }
            return new List<UserMailBox>();
        }

        public async Task<bool> ReadUserMailBox(UserMailBox mailBox)
        {
            var responce = await _httpClient.GetAsync("UserMailBox/ReadUserMailBox");

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
