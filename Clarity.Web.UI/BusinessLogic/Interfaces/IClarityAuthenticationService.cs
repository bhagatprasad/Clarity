using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthResponse> Authenticateuser(Authenticateuser authenticateuser);
        Task<ApplicationUser> GenarateUserClaims(AuthResponse authResponse);
    }
}
