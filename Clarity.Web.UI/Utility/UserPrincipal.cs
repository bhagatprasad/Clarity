using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Clarity.Web.UI.Utility
{
    public class UserPrincipal
    {
        public static ClaimsPrincipal GenarateUserPrincipal(ApplicationUser user)
        {
            var claims = new List<Claim>
          {
             new Claim("Id", user.Id.ToString()),
             new Claim("Phone", user.Phone),
             new Claim("Email", user.Email),
             new Claim("FirstName", user.FirstName),
             new Claim("LastName", user.LastName)
          };
            var principal = new ClaimsPrincipal();
            principal.AddIdentity(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            return principal;
        }
    }
}
