using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface ITimesheetService
    {
        Task<bool> InsertOrUpdateTimesheet(Timesheet timesheet);
        Task<bool> TimesheetStatusChangeProcess(TimesheetStatusChange timesheetStatusChange);
        Task<List<Timesheet>> GetAllTimesheetsAsync();
        Task<List<Timesheet>> GetAllTimesheetsAsync(long userId);
        Task<List<Timesheet>> GetAllTimesheetsAsync(string status);
        Task<List<Timesheet>> GetAllTimesheetsAsync(long userId, string status);
        Task<List<Timesheet>> GetAllTimesheetsAsync(DateTimeOffset? fromdate, DateTimeOffset? todate);
        Task<List<Timesheet>> GetAllTimesheetsAsync(long userId, DateTimeOffset? fromdate, DateTimeOffset? todate);
        Task<UserPendingAndAprovedTimesheet> FetchUserPaindingAndApprovedHrs(long userId);

    }
}
