using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Newtonsoft.Json;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class TaskCodeService : ITaskCodeService
    {
        private readonly HttpClient httpClient;
        public TaskCodeService(HttpClientService httpClientService)
        {
            httpClient = httpClientService.GetHttpClient();
        }

        public async Task<bool> CreateTaskCode(TaskCode _taskCode)
        {
            var taskCode = JsonConvert.SerializeObject(_taskCode);
            var requstContent = new StringContent(taskCode, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("TaskCode", requstContent);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var responceContent = JsonConvert.DeserializeObject<bool>(content);

                return responceContent ? responceContent : false;
            }
            return false;
        }


        public Task<bool> DeleteTaskCode(long TaskItemId)
        {
            throw new NotImplementedException();
        }
        public async Task<List<TaskCode>> GetAllTaskCode()
        {
            List<TaskCode> taskCode = new List<TaskCode>();

            var responce = await httpClient.GetAsync("TaskCode");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();

                var contentResponce = JsonConvert.DeserializeObject<List<TaskCode>>(content);

                return contentResponce != null ? contentResponce : taskCode;
            }
            return taskCode;
        }


        public async Task<TaskCode> GetTaskCode(long TaskCodeId)
        {
            var urlContent = Path.Combine("TaskCode", TaskCodeId.ToString());

            var responce = await httpClient.GetAsync(urlContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();

                var contentResponce = JsonConvert.DeserializeObject<TaskCode>(content);

                return contentResponce != null ? contentResponce : null;
            }
            return null;
        }

        public async Task<bool> UpdateTaskCode(long TaskCodeId, TaskCode _taskCode)
        {
            var urlContent = Path.Combine("TaskCode", TaskCodeId.ToString());

            var taskCode = JsonConvert.SerializeObject(_taskCode);

            var requstContent = new StringContent(taskCode, Encoding.UTF8, "application/json");

            var responce = await httpClient.PutAsync(urlContent, requstContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();

                var responceContent = JsonConvert.DeserializeObject<bool>(content);

                return responceContent ? responceContent : false;
            }

            return false;

        }


    }
}
