using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Admin")]
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
            try
            {
                var States = await stateService.GetStates();
                return View();

            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }

        }

        [HttpPost]

        public async Task<IActionResult> AddEditStates([FromBody] State state)
        {
            try
            {
                if (state != null)
                {
                    bool response = false;

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
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }

        }


        [HttpGet]
        public async Task<IActionResult> LoadStates()
        {
            try
            {
                var states = await stateService.GetStates();
                return Json(new { data = states });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
