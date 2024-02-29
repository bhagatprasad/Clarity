using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    public class StateController : Controller
    {
        private readonly IStateService stateService;
        private readonly INotyfService notyfService;

        public StateController(IStateService _stateService,
                                INotyfService _notyfService)

        {
            this.stateService = _stateService;
            this.notyfService = _notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var States = await stateService.GetAllStates();
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> AddEditStates([FromBody] State state)
        {
            if (state != null)
            {
                bool response = false;
                if (state.StateId > 0)
                    response = await stateService.UpdateState(state.StateId, state);
                else
                    response = await stateService.CreateState(state);
                if (response)
                {
                    if (state.StateId > 0)
                        notyfService.Success("State was updated successfully");
                    else
                        notyfService.Success("State was created successfully");
                    return Json(true);

                }
                notyfService.Warning("States Not Found");
                return Json(response);

            }
            notyfService.Warning("Something went wrong");
            return Json(false);
        }


        [HttpGet]
        public async Task<IActionResult> LoadStates()
        {
            var states = await stateService.GetAllStates();
            return Json(new { data = states });
        }
    }
}
