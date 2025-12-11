using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Admin")]
    public class TaskCodeController : Controller
    {
        private readonly ITaskCodeService taskCodeService;
        private readonly INotyfService notyfService;

        public TaskCodeController(ITaskCodeService taskCodeService, INotyfService notyfService)
        {
            this.taskCodeService = taskCodeService;
            this.notyfService = notyfService;

        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LoadTaskCodes()
        {
            var taskItems = await taskCodeService.GetAllTaskCode();

            return Json(new { data = taskItems });
        }

        [HttpPost]
        public async Task<IActionResult> AddEditTaskCode([FromBody] TaskCode taskCode)
        {
            if (taskCode != null)
            {
                bool responce = false;

                if (taskCode.TaskCodeId > 0)
                    responce = await taskCodeService.UpdateTaskCode(taskCode.TaskCodeId,taskCode);
                else
                    responce = await taskCodeService.CreateTaskCode(taskCode);

                if (responce)
                {
                    if (taskCode.TaskCodeId > 0)
                        notyfService.Success("TaskCode was Updated successfully");
                    else
                        notyfService.Success("TaskCode was Created successfully");

                    return Json(true);
                }
                notyfService.Error("Somwthing went wrong");
                return Json(responce);
            }

            notyfService.Error("Somwthing went wrong");

            return Json(false);
        }
    }
}
