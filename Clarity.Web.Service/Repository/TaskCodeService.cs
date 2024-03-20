using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class TaskCodeService : ITaskCodeService
    {
        private readonly ApplicationDBContext dbContext;
        public TaskCodeService(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> CreateTaskCode(TaskCode taskCode)
        {
            if(taskCode != null)
                await dbContext.taskCodes.AddAsync(taskCode);
            var response=await dbContext.SaveChangesAsync();
            return response ==1?true:false;
        }

        public async Task<bool> DeleteTaskCode(long taskCodeId)
        {
            var taskcode=await dbContext.taskCodes.FindAsync(taskCodeId);
            if(taskcode != null)
                dbContext.taskCodes.Remove(taskcode);
            var response = await dbContext.SaveChangesAsync();
            return response == 1 ? true : false;
        }

        public async Task<List<TaskCode>> GetAllTaskCodes()
        {
            return await dbContext.taskCodes.Where(x=>x.IsActive).ToListAsync();
        }

        public async Task<TaskCode> GetTaskCode(long taskCodeId)
        {
            var taskCode = await dbContext.taskCodes.FindAsync(taskCodeId);
            if(taskCode != null)
                return taskCode;
            return null;
        }
        
        public async Task<bool> UpdateTaskCode(long taskCodeId, TaskCode _taskCode)
        {
            var taskCode = await dbContext.taskCodes.FindAsync(taskCodeId);
            if(taskCode != null)
            {
                taskCode.Name= _taskCode.Name;
                taskCode.Code= _taskCode.Code;
                taskCode.TaskItemId= _taskCode.TaskItemId;
                taskCode.CreatedBy= _taskCode.CreatedBy;
                taskCode.CreatedOn= _taskCode.CreatedOn;
                taskCode.ModifiedBy= _taskCode.ModifiedBy;
                taskCode.ModifiedOn= _taskCode.ModifiedOn;
                taskCode.IsActive= _taskCode.IsActive;
            }
            var response = await dbContext.SaveChangesAsync();
            return response == 1 ? true : false;
        }
    }
}
