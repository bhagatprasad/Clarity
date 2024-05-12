using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Newtonsoft.Json;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class MessageTypeService : IMessageTypeService
    {
        private readonly HttpClient _httpClient;
        public MessageTypeService(HttpClientService httpClientService)
        {
            _httpClient= httpClientService.GetHttpClient();
        }
        public async Task<List<MessageType>> GetAllMessageTypes()
        {
            var response = await _httpClient.GetAsync("MessageType/GetAllMessageTypes");
            if(response.IsSuccessStatusCode)
            {
                var content=await response.Content.ReadAsStringAsync();
                var contentResponse=JsonConvert.DeserializeObject<List<MessageType>>(content);
                return contentResponse!=null ? contentResponse : new List<MessageType>();
            }
            return new List<MessageType>();
        }
    }
}
