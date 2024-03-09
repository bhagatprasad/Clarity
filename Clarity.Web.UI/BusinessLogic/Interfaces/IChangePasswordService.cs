using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IChangePasswordService
    {
        Task<bool> fnChangePasswordAsync(ChangePassword changePassword);
        Task<ApplicationUser> fnForgotPasswordAsync(string email, string phone);
        Task<bool> fnResetPasswordAsync(ResetPassword resetPassword);
    }
}
