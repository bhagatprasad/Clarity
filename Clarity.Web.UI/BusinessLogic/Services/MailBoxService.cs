using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Newtonsoft.Json;
using System.Text;

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

        public async Task<MailBox> InsertMailMessageForClientAsync(MailBox mailBox)
        {
            var inputContent = JsonConvert.SerializeObject(mailBox);
            var requestContent = new StringContent(inputContent, Encoding.UTF8, "application/json");
            var responce = await httpClient.PostAsync("MailBox/InsertMailMessageAsync", requestContent);

            if(responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var mail = JsonConvert.DeserializeObject<MailBox>(content);
                return mail;
            }
            return null;
        }
    }
}
