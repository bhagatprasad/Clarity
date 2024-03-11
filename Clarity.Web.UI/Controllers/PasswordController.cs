using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.Account;

namespace Clarity.Web.UI.Controllers
{

    public class PasswordController : Controller
    {
        private readonly INotyfService notyfService;
        private readonly IChangePasswordService changePasswordService;
        public PasswordController(INotyfService notyfService,
            IChangePasswordService changePasswordService)
        {
            this.notyfService = notyfService;
            this.changePasswordService = changePasswordService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword changePassword)
        {
            try
            {
                if (changePassword != null)
                {
                    var responce = await changePasswordService.fnChangePasswordAsync(changePassword);
                    if (responce)
                        notyfService.Success("Password changed successfully");
                }
                notyfService.Error("something went wrong");

                return Json(false);
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email, string phone)
        {
            try
            {
                if (!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(phone))
                {
                    var responce = await changePasswordService.fnForgotPasswordAsync(email, phone);
                    if (responce != null)
                    {
                        return Json(new { data = responce });
                    }
                }

                notyfService.Error("something went wrong");
                return Json(false);
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }

        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
        {
            try
            {
                if (resetPassword != null)
                {
                    var responce = await changePasswordService.fnResetPasswordAsync(resetPassword);
                    if (responce)
                        notyfService.Success("Password reseted successfully");
                }
                notyfService.Error("something went wrong");
                return Json(false);
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
