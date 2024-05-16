using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Admin")]
    public class MailBoxController : Controller
    {
        private readonly IMailBoxService mailBoxService;
        private readonly INotyfService notyfService;

        public MailBoxController(IMailBoxService mailBoxService,
            INotyfService notyfService)
        {
            this.mailBoxService = mailBoxService;
            this.notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> fetchAllMailBoxes()
        {
            try
            {
                var responce = await mailBoxService.GetMailBoxClinetAsync();
                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);

                throw ex;
            }
        }
    }
}
