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
    public class StateController : ControllerBase
    {
        private readonly IStateService stateService;
        public StateController(IStateService stateService)
        {
            this.stateService = stateService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateState(State state)
        {
            try
            {
                string statuscode = "";
                var verifyState = await stateService.VerifyStateAlreadyExists(state.Name);
                if (verifyState)
                    statuscode = "100";
                if (!verifyState)
                {
                    await stateService.CreateState(state);
                    statuscode = "99";
                }
                return Ok(new HttpRequestResponseMessage<string>(statuscode));
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Saving State", 500, ex.Message);
            }
        }
        [Route("{stateId:long}")]
        [HttpPut]
        public async Task<IActionResult> UpdateState(long stateId, State state)
        {
            try
            {
                await stateService.UpdateState(stateId, state);
                return Ok(true);
                var data = await stateService.UpdateState(stateId, state);
                return Ok(new HttpRequestResponseMessage<bool>(data));
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Updated State", 500, ex.Message);
            }
        }
        [Route("{stateId:long}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteState(long stateId)
        {
            try
            {
                var data = await stateService.DeleteState(stateId);
                return Ok(new HttpRequestResponseMessage<bool>(data));
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Delete State", 500, ex.Message);
            }
        }
        [HttpGet("{stateId}")]
        public async Task<IActionResult> GetState(long stateId)
        {
            try
            {
                var data = await stateService.GetState(stateId);
                return Ok(new HttpRequestResponseMessage<State>(data));
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Getting State", 500, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllState()
        {
            try
            {
                var state = await stateService.GetAllState();
                return Ok(new HttpRequestResponseMessage<List<State>>(state));
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error retrieving State", 500, ex.Message);

            }
        }
    }
}
