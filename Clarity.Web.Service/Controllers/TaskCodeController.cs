using Clarity.Web.Service.Helpers;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCodeController : ControllerBase
    {
        private readonly ITaskCodeService taskCodeService;
        public TaskCodeController(ITaskCodeService taskCodeService)
        {
            this.taskCodeService = taskCodeService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTaskCode(TaskCode taskCode)
        {
            try
            {
                    await taskCodeService.CreateTaskCode(taskCode);
                    return Ok(true);
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Saving TaskCode", 500, ex.Message);
            }
        }
        [Route("{taskCodeId:long}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTaskCode(long taskCodeId)
        {
            try
            {
                var response = await taskCodeService.DeleteTaskCode(taskCodeId);
                return Ok(response);
            }
            catch (Exception ex)
            {

                throw new HttpRequestExceptionMessage("Error Saving TaskCode", 500, ex.Message); ;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTaskCode()
        {
            try
            {
                var response = await taskCodeService.GetAllTaskCodes();
                return Ok(response);
            }
            catch (Exception ex)
            {

                throw new HttpRequestExceptionMessage("Error Retrieving TaskCodes",500,ex.Message);
            }
        }
        [Route("{taskCodeId}")]
        [HttpGet]
        public async Task<IActionResult> GetTaskCodeById(long taskCodeId)
        {
            try
            {
                var taskCode = await taskCodeService.GetTaskCode(taskCodeId);
                return Ok(taskCode);
            }
            catch (Exception ex)
            {

                throw new HttpRequestExceptionMessage("Error Retrieving TaskCode",500,ex.Message);
            }
        }
        [Route("{taskCodeId}")]
        [HttpPut]
        public async Task<IActionResult> UpdateTaskCodeById(long taskCodeId, TaskCode taskCode)
        {
            try
            {
                var response = await taskCodeService.UpdateTaskCode(taskCodeId, taskCode);
                return Ok(response);
            }
            catch (Exception ex)
            {

                throw new HttpRequestExceptionMessage("Error Updating TaskCode",500,ex.Message);
            }
        }
    }
}
