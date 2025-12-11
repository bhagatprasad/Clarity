using Clarity.Web.Service.Helpers;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ClarityAuthorize]
    public class UserMailBoxController : ControllerBase
    {
        private readonly IUserMailBoxService userMailBoxService;
        public UserMailBoxController(IUserMailBoxService _userMailBoxService)
        {
            this.userMailBoxService = _userMailBoxService;  
        }

        [HttpGet]
        [Route("GetAllUserMailBoxAsync")]
        public async Task<IActionResult> GetAllUserMailBoxAsync()
        {
            try
            {
                var usermailbox = await userMailBoxService.GetAllUserMailBoxAsync();
                return Ok(usermailbox);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAllUserMailBoxByID/{userId}")]
        public async Task<IActionResult> GetAllUserMailBoxByID(long userId)
        {
            try
            {
                var userMailBox = await userMailBoxService.GetAllUserMailBoxAsync(userId);
                return Ok(userMailBox);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpGet]
        [Route("ReadUserMailBox")]
        public async Task<IActionResult> ReadUserMailBox(UserMailBox userMailBox)
        {
            try
            {
                await userMailBoxService.ReadUserMailBox(userMailBox);
                return Ok(userMailBox);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }


    }
}
