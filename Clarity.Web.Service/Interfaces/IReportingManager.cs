using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IReportingManager
    {
        Task<bool> CreateReportingManager(RepotingManager manager);
        Task<bool> UpdateReportingManager(long employeeId, RepotingManager reportingManager);
        Task<ReportingManagerVM> FetchReportingManager(long employeeId);
        Task<List<ReportingManagerVM>> FetchAllReportingManager();
        Task<List<ReportingManagerVM>> FetchAllEmployeesByReportingManager(long managerId);
    }
}
