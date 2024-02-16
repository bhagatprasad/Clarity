using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class RolesService : IRolesService
    {
        private readonly HttpClient _httpClient = null;

        private readonly CoreConfig coreConfig;

        public RolesService(IOptions<CoreConfig> _coreConfig)
        {
            _httpClient = new HttpClient();
            coreConfig = _coreConfig.Value;
            _httpClient.BaseAddress = new Uri(coreConfig.BaseUrl);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout.Add(new TimeSpan(0, 0, 60));
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

        public Task<bool> InsertOrUpdateRole(Roles roles)
        {
            throw new NotImplementedException();
        }
    }
}
