using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Newtonsoft.Json;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class ChangePasswordService : IChangePasswordService
    {
        private readonly HttpClient httpClient;
        public ChangePasswordService(HttpClientService httpClientService)
        {
            httpClient = httpClientService.GetHttpClient();
        }
        public Task<bool> fnChangePasswordAsync(ChangePassword changePassword)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> fnForgotPasswordAsync(string email, string phone)
        {
            var userName = email != null ? email : phone;
            var uri = Path.Combine("Auth/ForgotPassword", userName);
            var response = await httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responceContent = JsonConvert.DeserializeObject<ApplicationUser>(content);
                return responceContent != null ? responceContent : new ApplicationUser();
            }
            return new ApplicationUser();
        }

        public Task<bool> fnResetPasswordAsync(ResetPassword resetPassword)
        {
            throw new NotImplementedException();
        }
    }
}
