using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface ITaskItemService
    {
        Task<bool> CreateTaskItem(TaskItem taskItem);
        Task<bool> DeleteTaskItem(long TaskItemId);
        Task<bool> UpdateTaskItem(long TaskItemId, TaskItem taskItem);
        Task<TaskItem> GetTaskItemById(long TaskItemId);
        Task<List<TaskItem>> GetAllTaskItems();
    }
}
