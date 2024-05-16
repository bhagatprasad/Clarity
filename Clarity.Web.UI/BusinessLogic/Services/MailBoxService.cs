using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Newtonsoft.Json;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class MailBoxService : IMailBoxService
    {
        private readonly HttpClient httpClient;

        public MailBoxService(HttpClientService httpClientService)
        {
            httpClient = httpClientService.GetHttpClient();
        }

        public async Task<List<MailBox>> GetMailBoxClinetAsync()
        {
            var response = await httpClient.GetAsync("MailBox/GetMailBoxesAsync");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var mailboxes = JsonConvert.DeserializeObject<List<MailBox>>(content);

                return mailboxes;
            }

            return null;
        }
    }
}
