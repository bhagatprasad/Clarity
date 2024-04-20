using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Admin")]
    public class TaskItemController : Controller
    {
        private readonly ITaskItemService taskItemService;
        private readonly INotyfService notyfService;

        public TaskItemController(ITaskItemService taskItemService, INotyfService notyfService)
        {
            this.taskItemService = taskItemService;
            this.notyfService = notyfService;

        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LoadTaskItems()
        {
            var taskItems = await taskItemService.GetAllTaskItems();

            return Json(new { data = taskItems });
        }

        [HttpPost]
        public async Task<IActionResult> AddEditTaskItem([FromBody] TaskItem taskItem)
        {
            if (taskItem != null)
            {
                bool responce = false;

                if (taskItem.TaskItemId > 0)
                    responce = await taskItemService.UpdateTaskItem(taskItem.TaskItemId, taskItem);
                else
                    responce = await taskItemService.CreateTaskItem(taskItem);

                if (responce)
                {
                    if (taskItem.TaskItemId > 0)
                        notyfService.Success("TaskItem was Updated successfully");
                    else
                        notyfService.Success("TaskItem was Created successfully");

                    return Json(true);
                }
                notyfService.Error("Somwthing went wrong");
                return Json(responce);
            }

            notyfService.Error("Somwthing went wrong");

            return Json(false);
        }
        [HttpGet]
        public async Task<IActionResult> fetchAllTaskItems()
        {
            try
            {
                var taskitems = await taskItemService.GetAllTaskItems();

                return Json(new { data = taskitems });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
