using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IAuthService
    {
       Task<AuthResponse> AuthenticateUser(string username, string password);
        Task<ApplicationUser> GenarateUserClaims(AuthResponse authResponse);
        Task<ApplicationUser> ForgotPassword(string userName);
        Task<bool> ResetPasswordAsync(ResetPassword resetPassword);
    }
}
