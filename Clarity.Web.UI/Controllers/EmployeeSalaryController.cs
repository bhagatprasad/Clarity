using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.BusinessLogic.Services;
using Clarity.Web.UI.Models;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.Account;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Admin")]
    public class EmployeeSalaryController : Controller
    {
        private readonly IEmployeeSalaryService employeeSalaryService;
        private readonly INotyfService notyfService;
        private readonly IDocumentService documentService;
        private readonly IDesignationService designationService;
        private readonly IDepartmentService departmentService;
        private readonly IEmployeeSalaryStructureService employeeSalaryStructureService;
        private static readonly ILog log = LogManager.GetLogger(typeof(EmployeeController));

        public EmployeeSalaryController(IEmployeeSalaryService _employeeSalaryService, INotyfService _notyfService,
            IDocumentService documentService,
            IDesignationService designationService, IDepartmentService departmentService,
            IEmployeeSalaryStructureService employeeSalaryStructureService)
        {
            this.employeeSalaryService = _employeeSalaryService;
            this.notyfService = _notyfService;
            this.documentService = documentService;
            this.designationService = designationService;
            this.departmentService = departmentService;
            this.employeeSalaryStructureService = employeeSalaryStructureService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public async Task<IActionResult> FetchAllEmployeeSalaries()
        {
            try
            {
                var salaries = await employeeSalaryService.FetchAllEmployeeSalaries(null, null, null, null, 0);
               
                return Json(new { data = salaries });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                log.Error("FetchAllEmployeeSalaries.." + ex);
                throw ex;
            }

        }

        [HttpGet]
        public async Task<IActionResult> FetchEmployeeSalary(long employeeSalaryId)
        {
            try
            {
                var salaries = await employeeSalaryService.FetchEmployeeSalary(employeeSalaryId);
                return Json(new { data = salaries });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                log.Error("FetchEmployeeSalary.." + ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateEmployeeSalary([FromBody] EmployeeSalary employeeSalary)
        {
            try
            {
                var _employeeSalary = await employeeSalaryService.InsertOrUpdateEmployeeSalaryAsync(employeeSalary);
                return Json(new { data = _employeeSalary });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                log.Error("FetchEmployeeSalary.." + ex);
                throw ex;
            }
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

                log.Error("DownloadPaySlip.." + ex);

                throw ex;
            }

        }
    }
}
