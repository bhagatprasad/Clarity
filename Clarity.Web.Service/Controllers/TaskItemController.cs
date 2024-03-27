using Clarity.Web.Service.Helpers;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ClarityAuthorize]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemService taskItemService;
        public TaskItemController(ITaskItemService taskItemService)
        {
            this.taskItemService = taskItemService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTaskItem(TaskItem taskItem)
        {
            try
            {
                await taskItemService.CreateTaskItem(taskItem);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Saving TaskItem", 500, ex.Message);
            }
        }

        [Route("{taskItemId:long}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTaskItem(long taskItemId)
        {
            try
            {
                var data = await taskItemService.DeleteTaskItem(taskItemId);
                return Ok(new HttpRequestResponseMessage<bool>(data));
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Delete TaskItem", 500, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTaskItems()
        {
            try
            {
                var taskItems =await taskItemService.GetAllTaskItems();
                return Ok(taskItems);
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error retrieving TaskItems", 500, ex.Message);
            }
        }
        [HttpGet("{taskItemId}")]
        public async Task<IActionResult> GetTaskItemById(long taskItemId)
        {
            try
            {
                var taskItem = await taskItemService.GetTaskItem(taskItemId);
                return Ok(taskItem);
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error retrieving TaskItem", 500, ex.Message);
            }
        }
       
        [HttpPut]
        [Route("{taskItemId}")]
        public async Task<IActionResult> UpdateTaskItem(long taskItemId, TaskItem taskItem)
        {
            try
            {
                var response = await taskItemService.UpdateTaskItem(taskItemId, taskItem);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Updating TaskItem", 500, ex.Message);
            }
        }
    }
}
