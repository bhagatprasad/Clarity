using Clarity.Web.Service.Helpers;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService departmentServices;
        public DepartmentController(IDepartmentService departmentServices)
        {
            this.departmentServices = departmentServices;
        }
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(Department department)
        {
            try
            {
                string statuscode = "";
                var verifyDepartment = await departmentServices.VerifyDepartmentAlreadyExists(statuscode);
                if (verifyDepartment)
                    statuscode = "100";
                if (!verifyDepartment)
                {
                    await departmentServices.CreateDepartment(department);
                    statuscode = "99";
                }
                return Ok(new HttpRequestResponseMessage<string>(statuscode));

            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Saving Department", 500, ex.Message);
            }
        }
        [Route("{departmentId:long}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDepartment(long departmentId)
        {
            try
            {
                var department = await departmentServices.DeleteDepartment(departmentId);
                return Ok(new HttpRequestResponseMessage<bool>(department));
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Delete Department", 500, ex.Message);
            }
        }
        [Route("{departmentId:long}")]
        [HttpPut]
        public async Task<IActionResult> UpdateDepartment(long departmentId, Department department)
        {
            try
            {
                await departmentServices.UpdateDepartment(departmentId, department);
                return Ok(true);
                var data = await departmentServices.UpdateDepartment(departmentId, department);
                return Ok(new HttpRequestResponseMessage<bool>(data));
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Updating Department", 500, ex.Message);
            }
        }
        [HttpGet("{departmentId:long}")]
        public async Task<IActionResult> GetDepartment(long departmentId)
        {
            try
            {
                var department = await departmentServices.GetDepartment(departmentId);
                return Ok(new HttpRequestResponseMessage<Department>(department));
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Getting Department", 500, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDepartment()
        {
            try
            {
                var department = await departmentServices.GetAllDepartment();
                return Ok(new HttpRequestResponseMessage<List<Department>>(department));
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error retrieving Department", 500, ex.Message);

            }
        }
    }
}
