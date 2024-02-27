using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clarity.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AccountController(IAuthenticationService authenticationService,
            IHttpContextAccessor httpContextAccessor)
        {
            this.authenticationService = authenticationService;
            this.httpContextAccessor = httpContextAccessor;
        }

       [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Authenticateuser authenticateuser)
        {
            try
            {
                var responce = await authenticationService.Authenticateuser(authenticateuser);

                if (responce != null)
                {
                    if (!string.IsNullOrEmpty(responce.JwtToken))
                    {
                        httpContextAccessor.HttpContext.Session.SetString("AccessToken", responce.JwtToken);

                        var claimsIdentity = await authenticationService.GenarateUserClaims(responce);

                        await HttpContext.SignInAsync(
                                                          CookieAuthenticationDefaults.AuthenticationScheme,
                                                           new ClaimsPrincipal(claimsIdentity),
                                                           new AuthenticationProperties
                                                           {
                                                               IsPersistent = true,
                                                               ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                                                           });

                        return RedirectToAction("Index", "Roles", null);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }
    }
}
