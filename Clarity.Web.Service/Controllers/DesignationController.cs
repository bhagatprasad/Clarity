using Clarity.Web.Service.Helpers;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Clarity.Web.Service.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ClarityAuthorize]
    public class DesignationController : ControllerBase
    {
        private  readonly IDesignationService designationService;
        public DesignationController(IDesignationService designationService) 
        {
            this.designationService = designationService;
        }

        [HttpPost]
        [Route("CreateDesignation")]
        public async Task<IActionResult> CreateDesignation(Designation designation)
        {
            try
            {
                await designationService.CreateDesignation(designation);
                return Ok(true);

            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Saving Designation", 500, ex.Message);
            }
        }
        [Route("{designationId:long}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDesignation(long designationId)
        {
            try
            {
                var designation = await designationService.DeleteDesignation(designationId);
                return Ok(new HttpRequestResponseMessage<bool>(designation));
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Delete Designation", 500, ex.Message);
            }
        }
        [Route("{designationId:long}")]
        [HttpPut]
        public async Task<IActionResult> UpdateDesignation(long designationId, Designation designation)
        {
            try
            {
                await designationService.UpdateDesignation(designationId, designation);
                return Ok(true);
                var data = await designationService.UpdateDesignation(designationId, designation);
                return Ok(new HttpRequestResponseMessage<bool>(data));
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Updating Designation", 500, ex.Message);
            }
        }
        [HttpGet("{designationId:long}")]
        public async Task<IActionResult> GetDesignation(long designationId)
        {
            try
            {
                var designation = await designationService.GetDesignation(designationId);
                return Ok(new HttpRequestResponseMessage<Designation>(designation));
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Getting Designation", 500, ex.Message);
            }
        }
        [HttpGet]
        [Route("GetAllDesignation")]
        public async Task<IActionResult> GetAllDesignation()
        {
            try
            {
                var designation = await designationService.GetAllDesignation();
                return Ok(designation);
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error retrieving Designation", 500, ex.Message);

            }
        }
    }
}
