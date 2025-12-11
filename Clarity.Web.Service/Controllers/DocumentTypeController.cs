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
    public class DocumentTypeController : ControllerBase
    {
        private readonly IDocumentTypeService _documentTypeService;

        public DocumentTypeController(IDocumentTypeService documentTypeService)
        {
            _documentTypeService = documentTypeService;
        }

        [HttpPost]
        [Route("InsertOrUpdateDocumentType")]
        public async Task<IActionResult> InsertOrUpdateDocumentType(DocumentType documentType)
        {
            try
            {
                var result = await _documentTypeService.InsertOrUpdateDocumentType(documentType);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("FetchAllDocumentTypes")]
        public async Task<IActionResult> FetchAllDocumentTypes()
        {
            try
            {
                var documentTypes = await _documentTypeService.FetchAllDocumentTypes();
                return Ok(documentTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
