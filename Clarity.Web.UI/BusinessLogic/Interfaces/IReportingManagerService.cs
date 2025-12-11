using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IReportingManagerService
    {
        Task<bool> CreateReportingManager(ReportingManager manager);
        Task<bool> UpdateReportingManager(long employeeId, ReportingManager reportingManager);
        Task<ReportingManagerVM> FetchReportingManager(long employeeId);
        Task<List<ReportingManagerVM>> FetchAllReportingManager();
        Task<List<ReportingManagerVM>> FetchAllEmployeesByReportingManager(long managerId);
    }
}
