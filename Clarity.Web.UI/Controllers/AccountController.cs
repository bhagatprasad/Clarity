using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clarity.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IClarityAuthenticationService authenticationService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly INotyfService notyfService;
        public AccountController(IClarityAuthenticationService authenticationService,
            IHttpContextAccessor httpContextAccessor,
            INotyfService notyfService)
        {
            this.authenticationService = authenticationService;
            this.httpContextAccessor = httpContextAccessor;
            this.notyfService = notyfService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Login([FromBody] Authenticateuser authenticateuser)
        {
            try
            {
                var responce = await authenticationService.Authenticateuser(authenticateuser);

                if (responce != null)
                {
                    if (!string.IsNullOrEmpty(responce.JwtToken))
                    {
                        httpContextAccessor.HttpContext.Session.SetString("AccessToken", responce.JwtToken);

                        var userClaimes = await authenticationService.GenarateUserClaims(responce);

                        var claimsIdentity = UserPrincipal.GenarateUserPrincipal(userClaimes);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                           new ClaimsPrincipal(claimsIdentity),
                                                           new AuthenticationProperties
                                                           {
                                                               IsPersistent = true,
                                                               ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                                                           });

                        return Json(new { appUser = userClaimes, status = true });
                    }

                    notyfService.Error(responce.StatusMessage);
                }
                else
                {
                    notyfService.Error("Something went wrong");
                }

            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
            }

            return Json(new { appUser = default(object), status = false });

        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account", null);
        }
    }
}
