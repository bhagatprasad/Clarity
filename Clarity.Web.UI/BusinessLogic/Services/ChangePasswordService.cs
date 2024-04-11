using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class ChangePasswordService : IChangePasswordService
    {
        private readonly CoreConfig coreConfig;
        private readonly HttpClient httpClient = null;
        public ChangePasswordService(IOptions<CoreConfig> options)
        {
            httpClient = new HttpClient();
            coreConfig = options.Value;
            httpClient.BaseAddress = new Uri(coreConfig.BaseUrl);
            httpClient.DefaultRequestHeaders.Clear();
        }
        public async Task<bool> fnChangePasswordAsync(ChangePassword changePassword)
        {
            var resetpassword = JsonConvert.SerializeObject(changePassword);

            var requestContent = new StringContent(resetpassword, Encoding.UTF8, "application/json");

            var responce = await httpClient.PostAsync("Auth/ChangePasswordAsync", requestContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();

                var responceContent = JsonConvert.DeserializeObject<bool>(content);

                return responceContent ? responceContent : false;
            }
            return false;
        }

        public async Task<ApplicationUser> fnForgotPasswordAsync(string email, string phone)
        {
            var userName = email != null ? email : phone;
            var uri = Path.Combine("Auth/ForgotPasswordAsync", userName);
            var response = await httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responceContent = JsonConvert.DeserializeObject<ApplicationUser>(content);
                return responceContent != null ? responceContent : new ApplicationUser();
            }
            return new ApplicationUser();
        }

        public async Task<bool> fnResetPasswordAsync(ResetPassword resetPassword)
        {
            var resetpassword = JsonConvert.SerializeObject(resetPassword);

            var requestContent = new StringContent(resetpassword, Encoding.UTF8, "application/json");

            var responce = await httpClient.PostAsync("Auth/ResetPasswordAsync", requestContent);

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
