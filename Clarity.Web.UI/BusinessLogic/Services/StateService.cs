using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Microsoft.Extensions.Options;
using Microsoft.TeamFoundation.Common;
using Newtonsoft.Json;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class StateService : IStateService
    {
        private readonly HttpClient httpClient;

        public StateService(HttpClientService httpClientService)
        {
            httpClient = httpClientService.GetHttpClient();
        }

        public async Task<bool> CreateState(State _state)
        {
            var state = JsonConvert.SerializeObject(_state);
            var requstContent = new StringContent(state, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("State", requstContent);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var responceContent = JsonConvert.DeserializeObject<bool>(content);

                return responceContent ? responceContent : false;
            }
            return false;
        }
        public async Task<List<State>> GetStates()
        {
            var states = new List<State>();

            var response = await httpClient.GetAsync("State");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var responceContent = JsonConvert.DeserializeObject<List<State>>(content);

                return responceContent != null ? responceContent : states;
            }
            return states;
        }
    }
}
