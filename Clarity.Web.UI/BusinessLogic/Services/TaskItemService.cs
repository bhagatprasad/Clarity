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
            httpClient= httpClientService.GetHttpClient();
        }
        public async Task<bool> CreateTaskItem(TaskItem _taskItem)
        {
            var taskItem = JsonConvert.SerializeObject(_taskItem);
            var requstContent = new StringContent(taskItem, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("TaskItem",requstContent);
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

        public Task<List<TaskItem>> GetAllTaskItems()
        {
            throw new NotImplementedException();
        }

        public Task<TaskItem> GetTaskItemById(long TaskItemId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTaskItem(long TaskItemId, TaskItem taskItem)
        {
            throw new NotImplementedException();
        }
    }
}
