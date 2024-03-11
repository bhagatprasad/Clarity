using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.BusinessLogic.Services;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public UserDashBoardController(IEmployeeSalaryService _employeeSalaryService, INotyfService _notyfService,
            IDocumentService documentService,
            IDesignationService designationService, IDepartmentService departmentService,
            IEmployeeSalaryStructureService employeeSalaryStructureService, ITenantService tenantService)
        {
            this.employeeSalaryService = _employeeSalaryService;
            this.notyfService = _notyfService;
            this.documentService = documentService;
            this.designationService = designationService;
            this.departmentService = departmentService;
            this.employeeSalaryStructureService = employeeSalaryStructureService;
            this.tenantService = tenantService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public async Task<IActionResult> FetchAllEmployeeSalaries(long userId)
        {
            var salaries = new List<EmployeeSalaryModel>();

            var employeeies = await tenantService.fetchUser(userId);

            if (employeeies != null && employeeies.EmployeeId.HasValue)
            {
                salaries = await employeeSalaryService.FetchEmployeeSalaryAsync(employeeies.EmployeeId.Value);
            }

            return Json(new { data = salaries });
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
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
            }
            return null;
        }
    }
}
