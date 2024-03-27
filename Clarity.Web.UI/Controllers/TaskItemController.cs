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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var taskitems = await taskItemService.GetAllTaskItems();

                return View(taskitems);
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItem taskItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    taskItem.CreatedBy = 1;
                    taskItem.CreatedOn = DateTimeOffset.Now;
                    taskItem.ModifiedBy = 1;
                    taskItem.ModifiedOn = DateTimeOffset.Now;
                    taskItem.IsActive = true;

                    bool response = await taskItemService.CreateTaskItem(taskItem);
                    if (response)
                    {
                        notyfService.Success("TaskItem was Inserted successfully");
                        return RedirectToAction("Index", "TaskItem", null);
                    }
                }

                notyfService.Error("Somwthing went wrong");

                return View(taskItem);

            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(long TaskItemId)
        {
            try
            {
                var responce = await taskItemService.GetTaskItemById(TaskItemId);

                return View(responce);
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TaskItem taskItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    taskItem.ModifiedBy = 1;
                    taskItem.ModifiedOn = DateTimeOffset.Now;

                    bool response = await taskItemService.UpdateTaskItem(taskItem.TaskItemId, taskItem);

                    if (response)
                    {
                        notyfService.Success("TaskItem was Updated successfully");

                        return RedirectToAction("Index", "TaskItem", null);
                    }
                }

                notyfService.Error("Somwthing went wrong");

                return View(taskItem);

            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
