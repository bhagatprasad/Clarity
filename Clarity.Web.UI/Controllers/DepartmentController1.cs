using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.BusinessLogic.Services;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Clarity.Web.UI.Controllers
{
    public class DepartmentController1 : Controller
    {
        private readonly IDepartmentService departmentServices;
        private readonly INotyfService notyfService;
        public DepartmentController1(DepartmentService _departmentServices,
                                    INotyfService _notyfService)
        {
            this.departmentServices = _departmentServices;
            this.notyfService = _notyfService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEditDepartment([FromBody] Department department)
        {
            if (department != null)
            {
                bool response = false;
                if (department.DepartmentId > 0)
                    response = await departmentServices.UpdateDepartment(department.DepartmentId, department);
                else
                    response = await departmentServices.CreateDepartment(department);
                if (response)
                {
                    if (department.DepartmentId > 0)
                        notyfService.Success("Department was updated successfully");
                    else
                        notyfService.Success("Department was created successfully");

                    return Json(true);
                }
                notyfService.Warning("Something went wrong");
                return Json(response);
            }
            notyfService.Warning("Something went wrong");
            return Json(false);
        }

        [HttpGet]
        public async Task<IActionResult> LoadDepartments()
        {
            var departments = await departmentServices.GetAllDepartment();
            return Json(new { data = departments });

        }
    }
}
