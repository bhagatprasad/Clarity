using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.Account;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Admin")]
    public class EmployeeDocumentController : Controller
    {
        private readonly IEmployeeDocumentService employeeDocumentService;
        private readonly INotyfService notyfService;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        public EmployeeDocumentController(IEmployeeDocumentService employeeDocumentService,
            INotyfService notyfService,
            IWebHostEnvironment hostingEnvironment,
            IHttpContextAccessor httpContextAccessor)
        {
            this.employeeDocumentService = employeeDocumentService;
            this.notyfService = notyfService;
            this.hostingEnvironment = hostingEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> fetchAllEmployeeDocuments()
        {
            var responce = await employeeDocumentService.FetchEmployeeDocumentsAsync();

            return Json(new { data = responce });
        }

        [HttpPost]
        public async Task<IActionResult> UploadEmployeeDocument(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                notyfService.Error("No file uploaded.");
                return Json(new { status = false, documentPath = "" });
            }

            try
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "documents");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return Json(new { status = true, documentPath = Path.Combine("documents", fileName) });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost]
        public async Task<IActionResult> upsertEmployementDocument([FromBody] EmployeeDocument eDocument)
        {
            if (eDocument != null)
            {

                var responce = await employeeDocumentService.InsertOrUpdateEmployeeDocument(eDocument);

                notyfService.Success("Document Inserted");

                return Json(responce);
            }

            notyfService.Error("Document NOT Inserted");

            return Json(false);
        }
        [HttpGet]
        public IActionResult DownloadFile(string relativeFilePath)
        {
            string webRootPath = hostingEnvironment.WebRootPath;

            string absoluteFilePath = Path.Combine(webRootPath, relativeFilePath);

            if (!System.IO.File.Exists(absoluteFilePath))
            {
                return NotFound();
            }
            return PhysicalFile(absoluteFilePath, "application/octet-stream", Path.GetFileName(absoluteFilePath));
        }
    }
}
