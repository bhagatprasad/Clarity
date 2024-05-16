using Clarity.Web.Service.Helpers;
using Clarity.Web.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ClarityAuthorize]
    public class MailBoxController : ControllerBase
    {
        private readonly IMailBoxService mailBoxService;
        public MailBoxController(IMailBoxService _mailBoxService)
        {
            this.mailBoxService = _mailBoxService;
        }

        [HttpGet]
        [Route("GetMailBoxesAsync")]
        public async Task<IActionResult> GetMailBoxesAsync()
        {
            try
            {
                var responce = await mailBoxService.GetMailBoxesAsync();

                return Ok(responce);

            }catch(Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
