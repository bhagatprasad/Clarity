using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface ITaskItemService
    {
        Task<bool> CreateTaskItem(TaskItem taskItem);
        Task<bool> UpdateTaskItem(long taskItemId, TaskItem taskItem);
        Task<bool> DeleteTaskItem(long taskItemId);
        Task<TaskItem> GetTaskItem(long taskItemId);
        Task<List<TaskItem>> GetAllTaskItems();
    }
}
