using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Helpers;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ClarityAuthorize]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService roleService;
        public RolesController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpGet]
        [Route("fetchAllRoles")]
        public async Task<IActionResult> fetchAllRoles()
        {
            try
            {
                var roles = await roleService.fetchAllRoles();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("InsertOrUpdateRole")]
        public async Task<ActionResult> InsertOrUpdateRole(Roles roles)
        {
            try
            {
                var responce = await roleService.InsertOrUpdateRole(roles);

                return Ok(responce);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpDelete]
        [Route("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(long id)
        {
            try
            {
                var responce = await roleService.DeleteRole(id);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("fetchRole/{id}")]
        public async Task<IActionResult> fetchRole(long id)
        {
            try
            {
                var responce = await roleService.fetchRole(id);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
