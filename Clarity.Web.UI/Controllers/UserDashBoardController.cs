using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.BusinessLogic.Services;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Software Engineer/Developer,Full-stack Developer")]
    public class UserDashBoardController : Controller
    {
        private readonly IEmployeeSalaryService employeeSalaryService;
        private readonly INotyfService notyfService;
        private readonly IDocumentService documentService;
        private readonly IDesignationService designationService;
        private readonly IDepartmentService departmentService;
        private readonly IEmployeeSalaryStructureService employeeSalaryStructureService;
        private readonly ITenantService tenantService;
        private readonly IEmployeeDocumentService employeeDocumentService;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly ITimesheetService timesheetService;

        public UserDashBoardController(IEmployeeSalaryService _employeeSalaryService, INotyfService _notyfService,
            IDocumentService documentService,
            IDesignationService designationService, IDepartmentService departmentService,
            IEmployeeSalaryStructureService employeeSalaryStructureService, ITenantService tenantService,
            IEmployeeDocumentService employeeDocumentService, IWebHostEnvironment hostingEnvironment, ITimesheetService timesheetService)
        {
            this.employeeSalaryService = _employeeSalaryService;
            this.notyfService = _notyfService;
            this.documentService = documentService;
            this.designationService = designationService;
            this.departmentService = departmentService;
            this.employeeSalaryStructureService = employeeSalaryStructureService;
            this.tenantService = tenantService;
            this.employeeDocumentService = employeeDocumentService;
            this.hostingEnvironment = hostingEnvironment;
            this.timesheetService = timesheetService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public async Task<IActionResult> FetchAllEmployeeSalaries(long userId)
        {
            try
            {
                var salaries = new List<EmployeeSalaryModel>();

                var employeeies = await tenantService.fetchUser(userId);

                if (employeeies != null && employeeies.EmployeeId.HasValue)
                {
                    salaries = await employeeSalaryService.FetchEmployeeSalaryAsync(employeeies.EmployeeId.Value);
                }

                return Json(new { data = salaries });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);

                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> FetchOfferLetter(long userId)
        {
            try
            {
                var documentType = EmployeeDocumentEnum.OfferLetter.ToString();

                List<EmployeeDocument> employeeDocuments = new List<EmployeeDocument>();

                employeeDocuments = await employeeDocumentService.FetchEmployeeDocumentsAsync("Offer Letter", userId);

                var employmentDocuments = await employeeDocumentService.FetchEmployeeDocumentsAsync("Experience Certificate", userId);
               
                if (employmentDocuments.Count > 0)
                    employeeDocuments = employeeDocuments.Concat(employmentDocuments).ToList();

                return Json(new { data = employeeDocuments });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);

                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> FetchAllHikesLetters(long userId)
        {
            try
            {
                var documentType = EmployeeDocumentEnum.HikeLetter.ToString();

                var offerLetter = await employeeDocumentService.FetchEmployeeDocumentsAsync("Hike Letter", userId);

                return Json(new { data = offerLetter });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);

                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> FetchAllFormSixteensLetters(long userId)
        {
            try
            {

                var offerLetter = await employeeDocumentService.FetchEmployeeDocumentsAsync("FORM 16", userId);

                return Json(new { data = offerLetter });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);

                throw ex;
            }
        }
        [HttpGet]
        public async Task<FileResult> GenaratePaySlip(long employeeSalaryId)
        {
            try
            {
                if (employeeSalaryId > 0)
                {
                    var salaries = await employeeSalaryService.FetchEmployeeSalary(employeeSalaryId);

                    Department department = null;

                    Designation designation = null;

                    if (salaries.employee.DepartmentId.HasValue)
                    {
                        var departments = await departmentService.GetAllDepartment();

                        department = departments.Where(x => x.DepartmentId == salaries.employee.DepartmentId.Value).FirstOrDefault();
                    }
                    if (salaries.employee.DesignationId.HasValue)
                    {
                        var designations = await designationService.GetAllDesignation();

                        designation = designations.Where(x => x.DesignationId == salaries.employee.DesignationId.Value).FirstOrDefault();
                    }

                    var salaryStructure = await employeeSalaryStructureService.fetchEmployeeSalaryStructure(salaries.employee.EmployeeId);

                    PayslipVM payslipVM = new PayslipVM();
                    payslipVM.employee = salaries.employee;
                    payslipVM.employeeSalary = salaries.employeeSalary;
                    payslipVM.designation = designation != null ? designation : null;
                    payslipVM.department = department != null ? department : null;
                    payslipVM.employeeSalaryStructure = salaryStructure != null ? salaryStructure : null;

                    string viewPath = "~/Views/Shared/_PayslipPartial.cshtml";

                    var pdfFile = documentService.GeneratePdfFromRazorView(viewPath, payslipVM);

                    string fileName = salaries.employee.EmployeeCode + "_" + salaries.employee.FirstName + "_" + "Payslip for the month" + "_" + salaries.employeeSalary.SalaryMonth + "_" + salaries.employeeSalary.SalaryYear + ".pdf";

                    return File(pdfFile, "application/pdf", fileName);
                }

                return null;
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);

                throw ex;
            }
        }
        [HttpGet]
        public IActionResult DownloadEmployeeDocument(string relativeFilePath)
        {
            try
            {
                string webRootPath = hostingEnvironment.WebRootPath;

                string absoluteFilePath = Path.Combine(webRootPath, relativeFilePath);

                if (!System.IO.File.Exists(absoluteFilePath))
                {
                    return NotFound();
                }
                return PhysicalFile(absoluteFilePath, "application/octet-stream", Path.GetFileName(absoluteFilePath));
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);

                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> FetchUserApprovedAndPendingTimesheet (long userId)
        {
            try
            {
                var response = await timesheetService.FetchUserPaindingAndApprovedHrs(userId);
                return Json(new{data = response});
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
            
        }

    }
}
