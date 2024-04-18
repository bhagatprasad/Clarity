using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Clarity.Web.Service.Repository
{
    public class TimesheetService : ITimesheetService
    {
        private readonly ApplicationDBContext dbcontext;
        public TimesheetService(ApplicationDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<List<Timesheet>> GetAllTimesheetsAsync()
        {
            return await dbcontext.timesheets.Include(t => t.timesheetTasks).ToListAsync();
        }

        public async Task<List<Timesheet>> GetAllTimesheetsAsync(long userId)
        {
            var userTimesheets = await dbcontext.timesheets.Where(x => x.UserId == userId).Include(t => t.timesheetTasks).ToListAsync();

            var employeeId = await dbcontext.users.Where(u => u.Id == userId).Select(u => u.EmployeeId).FirstOrDefaultAsync();

            if (employeeId != null)
            {
                var reportingEmployees = await dbcontext.reportingManagers.Where(rm => rm.ManagerId == employeeId).Select(rm => rm.EmployeeId).ToListAsync();

                if (reportingEmployees.Any())
                {
                    var userIDs = await dbcontext.users.Where(u => reportingEmployees.Contains(u.EmployeeId.Value)).Select(u => u.Id).ToListAsync();

                    if (userIDs.Any())
                    {
                        var otherTimesheets = await dbcontext.timesheets.Where(ts => userIDs.Contains(ts.UserId.Value)).ToListAsync();

                        userTimesheets.AddRange(otherTimesheets);
                    }
                }

            }

            return userTimesheets;
        }

        public async Task<List<Timesheet>> GetAllTimesheetsAsync(string status)
        {
            return await dbcontext.timesheets.Where(x => x.Status == status).Include(t => t.timesheetTasks).ToListAsync();
        }

        public async Task<List<Timesheet>> GetAllTimesheetsAsync(long userId, string status)
        {
            var userTimesheets = await dbcontext.timesheets.Where(x => x.UserId == userId && x.Status == status).Include(t => t.timesheetTasks).ToListAsync();

            var employeeId = await dbcontext.users.Where(u => u.Id == userId).Select(u => u.EmployeeId).FirstOrDefaultAsync();

            if (employeeId != null)
            {
                var reportingEmployees = await dbcontext.reportingManagers.Where(rm => rm.ManagerId == employeeId).Select(rm => rm.EmployeeId).ToListAsync();

                if (reportingEmployees.Any())
                {
                    var userIDs = await dbcontext.users.Where(u => reportingEmployees.Contains(u.EmployeeId.Value)).Select(u => u.Id).ToListAsync();

                    if (userIDs.Any())
                    {
                        var otherTimesheets = await dbcontext.timesheets.Where(ts => userIDs.Contains(ts.UserId.Value) && ts.Status == status).ToListAsync();

                        userTimesheets.AddRange(otherTimesheets);
                    }
                   
                }
                  
            }

            return userTimesheets;
        }

        public async Task<List<Timesheet>> GetAllTimesheetsAsync(DateTimeOffset? fromdate, DateTimeOffset? todate)
        {
            var response = await dbcontext.timesheets.Where(x => x.FromDate >= fromdate && x.ToDate <= todate).Include(t => t.timesheetTasks).ToListAsync();

            return response;
        }

        public async Task<List<Timesheet>> GetAllTimesheetsAsync(long userId, DateTimeOffset? fromdate, DateTimeOffset? todate)
        {
            var userTimesheets = await dbcontext.timesheets.Where(x => x.UserId == userId && x.FromDate >= fromdate && x.ToDate <= todate).Include(t => t.timesheetTasks).ToListAsync();

            var employeeId = await dbcontext.users.Where(u => u.Id == userId).Select(u => u.EmployeeId).FirstOrDefaultAsync();

            if (employeeId != null)
            {
                var reportingEmployees = await dbcontext.reportingManagers.Where(rm => rm.ManagerId == employeeId).Select(rm => rm.EmployeeId).ToListAsync();

                if (reportingEmployees.Any())
                {
                    var userIDs = await dbcontext.users.Where(u => reportingEmployees.Contains(u.EmployeeId.Value)).Select(u => u.Id).ToListAsync();

                    if(userIDs.Any())
                    {
                        var otherTimesheets = await dbcontext.timesheets.Where(ts => userIDs.Contains(ts.UserId.Value) && ts.FromDate >= fromdate && ts.ToDate <= todate).ToListAsync();

                        userTimesheets.AddRange(otherTimesheets);
                    }
                  
                }
                   
            }

            return userTimesheets;

        }

        public async Task<bool> InsertOrUpdateTimesheet(Timesheet timesheet)
        {
            if (timesheet != null)
                await dbcontext.timesheets.AddAsync(timesheet);

            var timesheetSaveResult = await dbcontext.SaveChangesAsync();

            if (timesheetSaveResult == 1)
            {
                foreach (var timesheetTask in timesheet.timesheetTasks)
                {
                    if (timesheetTask.Id == 0)
                    {
                        await dbcontext.timesheetTasks.AddAsync(timesheetTask);
                    }
                    else
                    {
                        dbcontext.timesheetTasks.Update(timesheetTask);
                    }
                }

                var timesheetTaskSaveResult = await dbcontext.SaveChangesAsync();

                return timesheetTaskSaveResult > 0;
            }
            return false;
        }

        public async Task<bool> TimesheetStatusChangeProcess(TimesheetStatusChange timesheetStatusChange)
        {
            var timesheet = await dbcontext.timesheets.FindAsync(timesheetStatusChange.TimesheetId);
            if (timesheet != null)
            {
                switch (timesheetStatusChange.Status)
                {
                    case "Approved":
                        timesheet.ApprovedBy = timesheetStatusChange.ModifiedBy;
                        timesheet.ApprovedOn = timesheetStatusChange.ModifiedOn;
                        timesheet.ApprovedComments = timesheetStatusChange.Comment;
                        timesheet.Status = timesheetStatusChange.Status;
                        break;
                    case "Cancelled":
                        timesheet.CancelledBy = timesheetStatusChange.ModifiedBy;
                        timesheet.CancelledOn = timesheetStatusChange.ModifiedOn;
                        timesheet.CancelledComments = timesheetStatusChange.Comment;
                        timesheet.Status = timesheetStatusChange.Status;
                        break;
                    case "Rejected":
                        timesheet.RejectedBy = timesheetStatusChange.ModifiedBy;
                        timesheet.RejectedOn = timesheetStatusChange.ModifiedOn;
                        timesheet.RejectedComments = timesheetStatusChange.Comment;
                        timesheet.Status = timesheetStatusChange.Status;
                        break;

                }

                var timesheetTaskSaveResult = await dbcontext.SaveChangesAsync();

                return timesheetTaskSaveResult > 0;
            }
            return false;
        }
    }
}
