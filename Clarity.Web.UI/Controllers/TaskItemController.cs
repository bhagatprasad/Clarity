using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    public class TaskItemController : Controller
    {
        private readonly ITaskItemService taskItemService;
        private readonly INotyfService notyfService;

        public TaskItemController(ITaskItemService taskItemService, INotyfService notyfService)
        {
            this.taskItemService = taskItemService;
            this.notyfService = notyfService;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTaskItem(TaskItem taskItem)
        {
            try
            {
               bool response= await taskItemService.CreateTaskItem(taskItem);
                if (response)
                {
                    notyfService.Success("TaskItem was Inserted successfully");
                    
                }
                notyfService.Warning("States Not Found");
                return Json(response);
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
