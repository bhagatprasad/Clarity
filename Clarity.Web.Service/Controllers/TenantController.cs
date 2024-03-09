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
    public class TenantController : ControllerBase
    {
        private readonly IUserService userService;
       
        public TenantController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterUser registerUser)
        {
            try
            {
                var responce = await userService.RegisterUser(registerUser);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("fetchUsers")]
        public async Task<IActionResult> fetchUsers()
        {
            try
            {
                var responce = await userService.fetchUsers();

                return Ok(responce);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("fetchUser/id")]
        public async Task<IActionResult> fetchUser(long id)
        {
            try
            {
                var responce = await userService.fetchUser(id);

                return Ok(responce);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
