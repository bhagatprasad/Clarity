using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class RolesService : IRolesService
    {
        private readonly HttpClient _httpClient;

        public RolesService(HttpClientService httpClientService)
        {
            _httpClient = httpClientService.GetHttpClient();
        }
        public Task<bool> DeleteRole(long roleId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Roles>> fetchAllRoles()
        {
            var responce = await _httpClient.GetAsync("Roles/fetchAllRoles");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<Roles>>(content);
                return contentReqponce != null ? contentReqponce : new List<Roles>();
            }
            return new List<Roles>();
        }

        public async Task<Roles> fetchRole(long roleId)
        {
            var urlcontent = Path.Combine("Roles/fetchRole", roleId.ToString());
            var responce = await _httpClient.GetAsync(urlcontent);
            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<Roles>(content);
                return contentReqponce != null ? contentReqponce : new Roles();
            }
            return new Roles();
        }

        public async Task<bool> InsertOrUpdateRole(Roles roles)
        {
            var inputContent = JsonConvert.SerializeObject(roles);

            var requestContent = new StringContent(inputContent, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("Roles/InsertOrUpdateRole", requestContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var responceContent = JsonConvert.DeserializeObject<bool>(content);
                return responceContent  ? responceContent : false;
            }
            return false;

        }
    }
}
