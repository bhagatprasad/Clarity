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
    public class EmployeeDocumentController : ControllerBase
    {
        private readonly IEmployeeDocumentService _employeeDocumentService;

        public EmployeeDocumentController(IEmployeeDocumentService employeeDocumentService)
        {
            _employeeDocumentService = employeeDocumentService;
        }

        [HttpPost]
        [Route("InsertOrUpdateEmployeeDocument")]
        public async Task<IActionResult> InsertOrUpdateEmployeeDocument([FromBody] EmployeeDocument employeeDocument)
        {
            try
            {
                var result = await _employeeDocumentService.InsertOrUpdateEmployeeDocument(employeeDocument);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("RemoveEmployeeDocument/{employeeDocumentId}")]
        public async Task<IActionResult> RemoveEmployeeDocument(long employeeDocumentId)
        {
            try
            {
                var result = await _employeeDocumentService.RemoveEmployeeDocument(employeeDocumentId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("FetchEmployeeDocumentsAsync")]
        public async Task<IActionResult> FetchEmployeeDocumentsAsync()
        {
            try
            {
                var documents = await _employeeDocumentService.FetchEmployeeDocumentsAsync();
                return Ok(documents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("FetchEmployeeDocumentsAsync/{id}")]
        public async Task<IActionResult> FetchEmployeeDocumentsAsync(long id)
        {
            try
            {
                var documents = await _employeeDocumentService.FetchEmployeeDocumentsAsync(id);
                return Ok(documents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("ByDocumentType/{documentType}")]
        public async Task<IActionResult> FetchEmployeeDocumentsAsync(string documentType)
        {
            try
            {
                var documents = await _employeeDocumentService.FetchEmployeeDocumentsAsync(documentType);
                return Ok(documents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("ByDocumentTypeAndIsActive/{documentType}/{isActive}")]
        public async Task<IActionResult> FetchEmployeeDocumentsAsync(long documentType, bool isActive)
        {
            try
            {
                var documents = await _employeeDocumentService.FetchEmployeeDocumentsAsync(documentType,isActive);
                return Ok(documents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("ByEmployeeAndDocumentType/{employeeId}/{documentType}")]
        public async Task<IActionResult> FetchEmployeeDocumentsAsync(long employeeId, long documentType)
        {
            try
            {
                var documents = await _employeeDocumentService.FetchEmployeeDocumentsAsync(employeeId, documentType);
                return Ok(documents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("ByEmployeeIdAndDocumentType/{employeeId}/{documentType}")]
        public async Task<IActionResult> FetchEmployeeDocumentsAsync(long employeeId, string documentType)
        {
            try
            {
                var documents = await _employeeDocumentService.FetchEmployeeDocumentsAsync(employeeId, documentType);
                return Ok(documents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("ByDocumentTypeAndUser/{documentType}/{userId}")]
        public async Task<IActionResult> FetchEmployeeDocumentsAsync(string documentType, long userId)
        {
            try
            {
                var documents = await _employeeDocumentService.FetchEmployeeDocumentsAsync(documentType, userId);
                return Ok(documents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("ByDocumentTypeAndEmail/{documentType}/{email}")]
        public async Task<IActionResult> FetchEmployeeDocumentsAsync(string documentType, string email)
        {
            try
            {
                var documents = await _employeeDocumentService.FetchEmployeeDocumentsAsync(documentType, email);
                return Ok(documents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
