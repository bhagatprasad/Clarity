using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.BusinessLogic.Services;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.Account;

namespace Clarity.Web.UI.Controllers
{
    [Authorize]
    public class EmployeeSalaryController : Controller
    {
        private readonly IEmployeeSalaryService employeeSalaryService;
        private readonly INotyfService notyfService;
        private readonly IDocumentService documentService;
        private readonly IDesignationService designationService;
        private readonly IDepartmentService departmentService;

        public EmployeeSalaryController(IEmployeeSalaryService _employeeSalaryService, INotyfService _notyfService,
            IDocumentService documentService,
            IDesignationService designationService, IDepartmentService departmentService)
        {
            this.employeeSalaryService = _employeeSalaryService;
            this.notyfService = _notyfService;
            this.documentService = documentService;
            this.designationService = designationService;
            this.departmentService = departmentService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public async Task<IActionResult> FetchAllEmployeeSalaries()
        {
            var salaries = await employeeSalaryService.FetchAllEmployeeSalaries(null, null, null, null, 0);
            return Json(new { data = salaries });
        }

        [HttpGet]
        public async Task<IActionResult> FetchEmployeeSalary(long employeeSalaryId)
        {
            var salaries = await employeeSalaryService.FetchEmployeeSalary(employeeSalaryId);
            return Json(new { data = salaries });
        }

        [HttpGet]
        public async Task<FileResult> DownloadPaySlip(long employeeSalaryId)
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

                    PayslipVM payslipVM = new PayslipVM();
                    payslipVM.employee = salaries.employee;
                    payslipVM.employeeSalary = salaries.employeeSalary;
                    payslipVM.designation = designation != null ? designation : null;
                    payslipVM.department = department != null ? department : null;

                    string viewPath = "~/Views/Shared/_PayslipPartial.cshtml";

                    var pdfFile = documentService.GeneratePdfFromRazorView(viewPath, payslipVM);

                    string fileName = salaries.employee.EmployeeCode + "_" + salaries.employee.FirstName + "_" + "Payslip for the month" + "_" + salaries.employeeSalary.SalaryMonth + "_" + salaries.employeeSalary.SalaryYear + ".pdf";
                    return File(pdfFile, "application/pdf", fileName);
                }
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
            }
            return null;
        }
    }
}
