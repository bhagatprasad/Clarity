using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Admin")]
    public class DocumentTypeController : Controller
    {
        private readonly IDocumentTypeService _documentTypeService;

        private readonly INotyfService _notyfService;

        public DocumentTypeController(IDocumentTypeService documentTypeService, INotyfService notyfService)
        {
            _documentTypeService = documentTypeService;
            _notyfService = notyfService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> FetchDocumentTypes()
        {
            try
            {
                var responce = await _documentTypeService.FetchAllDocumentTypes();
                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
