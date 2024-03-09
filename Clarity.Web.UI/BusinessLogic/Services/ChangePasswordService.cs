using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class ChangePasswordService : IChangePasswordService
    {
        public Task<bool> fnChangePasswordAsync(ChangePassword changePassword)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> fnForgotPasswordAsync(string email, string phone)
        {
            throw new NotImplementedException();
        }

        public Task<bool> fnResetPasswordAsync(ResetPassword resetPassword)
        {
            throw new NotImplementedException();
        }
    }
}
