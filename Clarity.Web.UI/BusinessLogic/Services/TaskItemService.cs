using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Newtonsoft.Json;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly HttpClient httpClient;

        public TaskItemService(HttpClientService httpClientService)
        {
            httpClient = httpClientService.GetHttpClient();
        }

        public async Task<bool> CreateTaskItem(TaskItem _taskItem)
        {
            var taskItem = JsonConvert.SerializeObject(_taskItem);
            var requstContent = new StringContent(taskItem, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("TaskItem", requstContent);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var responceContent = JsonConvert.DeserializeObject<bool>(content);

                return responceContent ? responceContent : false;
            }
            return false;
        }

        public Task<bool> DeleteTaskItem(long TaskItemId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TaskItem>> GetAllTaskItems()
        {
            List<TaskItem> taskItems = new List<TaskItem>();

            var responce = await httpClient.GetAsync("TaskItem");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();

                var contentResponce = JsonConvert.DeserializeObject<List<TaskItem>>(content);

                return contentResponce != null ? contentResponce : taskItems;
            }
            return taskItems;
        }

        public async Task<TaskItem> GetTaskItemById(long TaskItemId)
        {
            var urlContent = Path.Combine("TaskItem", TaskItemId.ToString());

            var responce = await httpClient.GetAsync(urlContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();

                var contentResponce = JsonConvert.DeserializeObject<TaskItem>(content);

                return contentResponce != null ? contentResponce : null;
            }
            return null;
        }

        public async Task<bool> UpdateTaskItem(long TaskItemId, TaskItem _taskItem)
        {
            var urlContent = Path.Combine("TaskItem", TaskItemId.ToString());

            var taskItem = JsonConvert.SerializeObject(_taskItem);

            var requstContent = new StringContent(taskItem, Encoding.UTF8, "application/json");

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
