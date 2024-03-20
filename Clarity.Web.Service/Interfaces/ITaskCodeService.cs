using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface ITaskCodeService
    {
        Task<bool> CreateTaskCode(TaskCode taskCode);
        Task<bool> UpdateTaskCode(long taskCodeId, TaskCode taskCode);
        Task<bool> DeleteTaskCode(long taskCodeId);
        Task<TaskCode> GetTaskCode(long taskCodeId);
        Task<List<TaskCode>> GetAllTaskCodes();
    }
}
