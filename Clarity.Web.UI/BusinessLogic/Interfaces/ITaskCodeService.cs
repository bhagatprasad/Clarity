using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface ITaskCodeService
    {
            Task<bool> CreateTaskCode(TaskCode taskCode);
            Task<bool> UpdateTaskCode(long TaskCodeId, TaskCode taskCode);
            Task<bool> DeleteTaskCode(long TaskCodeId);
            Task<TaskCode>GetTaskCode(long TaskCodeId);
            Task<List<TaskCode>> GetAllTaskCode();
     
    }
}
