using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ApplicationDBContext dbcontext;
        public TaskItemService(ApplicationDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<bool> CreateTaskItem(TaskItem taskItem)
        {
            if (taskItem != null)
                await dbcontext.taskItems.AddAsync(taskItem);
            var response = await dbcontext.SaveChangesAsync();
            return response == 1 ? true : false;
        }

        public async Task<bool> DeleteTaskItem(long taskItemId)
        {
            var taskItems = await dbcontext.taskItems.FindAsync(taskItemId);
            if (taskItems != null)
                dbcontext.taskItems.Remove(taskItems);
            var response = await dbcontext.SaveChangesAsync();
            return response == 1 ? true : false;
        }

        public async Task<List<TaskItem>> GetAllTaskItems()
        {
            return await dbcontext.taskItems.Where(x => x.IsActive).Include(x => x.taskCodes).ToListAsync();
        }

        public async Task<TaskItem> GetTaskItem(long taskItemId)
        {
            var taskItem = await dbcontext.taskItems.FindAsync(taskItemId);
            if (taskItem != null)
                return taskItem;
            return null;
        }

        public async Task<bool> UpdateTaskItem(long taskItemId, TaskItem _taskItem)
        {
            var taskItem = await dbcontext.taskItems.FindAsync(taskItemId);
            if (taskItem != null)
            {
                taskItem.Name = _taskItem.Name;
                taskItem.ModifiedOn = _taskItem.ModifiedOn;
                taskItem.ModifiedBy = _taskItem.ModifiedBy;
                taskItem.Code = _taskItem.Code;
            }

            var response = await dbcontext.SaveChangesAsync();

            return response == 1 ? true : false;
        }

    }
}
