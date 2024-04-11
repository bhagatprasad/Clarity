using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        [Route("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser(UserAuthentication userAuthentication)
        {
            try
            {
                var responce = await authService.AuthenticateUser(userAuthentication.username, userAuthentication.password);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("GenarateUserClaims")]
        public async Task<IActionResult> GenarateUserClaims(AuthResponse authResponse)
        {
            try
            {
                var responce = await authService.GenarateUserClaims(authResponse);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("ForgotPassword/{userName}")]
        public async Task<IActionResult>ForgotPassword(string userName)
        {
            try
            {
                var response = await authService.ForgotPassword(userName);
                return Ok(response);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
