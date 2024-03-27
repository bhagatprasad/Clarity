using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class ReportingManagerService : IReportingManager
    {
        private readonly ApplicationDBContext context;
        public ReportingManagerService(ApplicationDBContext dBContext)
        {
            this.context = dBContext;
        }

        public async Task<bool> CreateReportingManager(RepotingManager manager)
        {
            if (manager != null &&  await context.reportingManagers.AnyAsync(x =>x.EmployeeId != manager.EmployeeId))
            {
                await context.reportingManagers.AddAsync(manager);
            }
            var response = await context.SaveChangesAsync();

            return response == 1 ? true : false;
        }

        public async Task<List<ReportingManagerVM>> FetchAllEmployeesByReportingManager(long managerId)
        {
            var manager = new List<ReportingManagerVM>();
            manager = (from reporting in context.reportingManagers.Where(x => x.IsActive == true && x.ManagerId == managerId)
                       join employee in context.employees.Where(x => x.IsActive == true) on
                       reporting.EmployeeId equals employee.EmployeeId into employeejoin
                       from employeeInfo in employeejoin.DefaultIfEmpty()
                       join repomanager in context.employees.Where(x => x.IsActive == true) on
                       reporting.EmployeeId equals repomanager.EmployeeId into managerjoin
                       from managerinfo in managerjoin.DefaultIfEmpty()
                       select new ReportingManagerVM
                       {
                           RepotingManagerId = reporting.RepotingManagerId,
                           EmployeeId = employeeInfo != null ? employeeInfo.EmployeeId : 0,
                           EmployeeCode = employeeInfo != null ? employeeInfo.EmployeeCode : string.Empty,
                           EmployeeEmail = employeeInfo != null ? employeeInfo.Email : string.Empty,
                           EmployeeName = employeeInfo != null ? employeeInfo.FatherName + " " + employeeInfo.LastName : string.Empty,
                           ManagerId = managerinfo != null ? managerinfo.EmployeeId : 0,
                           ManagerCode = managerinfo != null ? managerinfo.EmployeeCode : string.Empty,
                           ManagerEmail = managerinfo != null ? managerinfo.Email : string.Empty,
                           ManagerName = managerinfo != null ? managerinfo.FirstName + " " + managerinfo.LastName : string.Empty

                       }).ToList();
            return manager;
        }

        public async Task<List<ReportingManagerVM>> FetchAllReportingManager()
        {
            var manager = new List<ReportingManagerVM>();
            manager = (from reporting in context.reportingManagers.Where(x => x.IsActive == true)
                       join employee in context.employees.Where(x =>x.IsActive== true) on
                       reporting.EmployeeId equals employee.EmployeeId into employeejoin from employeeInfo in employeejoin.DefaultIfEmpty()
                       join repomanager in context.employees.Where(x =>x.IsActive == true) on 
                       reporting.EmployeeId equals repomanager.EmployeeId into managerjoin from managerinfo in managerjoin.DefaultIfEmpty()
                       select new ReportingManagerVM
                       {
                          RepotingManagerId = reporting.RepotingManagerId,
                          EmployeeId = employeeInfo != null?employeeInfo.EmployeeId : 0,
                          EmployeeCode = employeeInfo != null?employeeInfo.EmployeeCode : string.Empty,
                          EmployeeEmail = employeeInfo !=null? employeeInfo.Email : string.Empty,
                          EmployeeName = employeeInfo != null? employeeInfo.FirstName + " " + employeeInfo.LastName : string.Empty,
                          ManagerId =  managerinfo != null? managerinfo.EmployeeId : 0,
                          ManagerCode = managerinfo != null?managerinfo.EmployeeCode : string.Empty,
                          ManagerEmail = managerinfo != null?managerinfo.Email : string.Empty,
                          ManagerName = managerinfo != null?managerinfo.FirstName + " "+ managerinfo.LastName : string.Empty

                       }).ToList();
            return manager;
        }

        public async Task<ReportingManagerVM> FetchReportingManager(long employeeId)
        {
            var manager = new ReportingManagerVM();
            manager = (from reporting in context.reportingManagers.Where(x => x.IsActive == true && x.EmployeeId == employeeId)
                       join employee in context.employees.Where(x => x.IsActive == true) on
                       reporting.EmployeeId equals employee.EmployeeId into employeejoin
                       from employeeInfo in employeejoin.DefaultIfEmpty()
                       join repomanager in context.employees.Where(x => x.IsActive == true) on
                       reporting.EmployeeId equals repomanager.EmployeeId into managerjoin
                       from managerinfo in managerjoin.DefaultIfEmpty()
                       select new ReportingManagerVM
                       {
                           RepotingManagerId = reporting.RepotingManagerId,
                           EmployeeId = employeeInfo != null ? employeeInfo.EmployeeId : 0,
                           EmployeeCode = employeeInfo != null ? employeeInfo.EmployeeCode : string.Empty,
                           EmployeeEmail = employeeInfo != null ? employeeInfo.Email : string.Empty,
                           EmployeeName = employeeInfo != null ? employeeInfo.FatherName + " " + employeeInfo.LastName : string.Empty,
                           ManagerId = managerinfo != null ? managerinfo.EmployeeId : 0,
                           ManagerCode = managerinfo != null ? managerinfo.EmployeeCode : string.Empty,
                           ManagerEmail = managerinfo != null ? managerinfo.Email : string.Empty,
                           ManagerName = managerinfo != null ? managerinfo.FirstName + " " + managerinfo.LastName : string.Empty

                       }).FirstOrDefault();
            return manager;
        }

        public async Task<bool> UpdateReportingManager(long employeeId, RepotingManager reportingManager)
        {
            var managers = await context.reportingManagers.FindAsync(employeeId);
            if (managers != null)
            {
                managers.EmployeeId = reportingManager.EmployeeId;
                managers.ManagerId = reportingManager.ManagerId;
                managers.CreatedOn = DateTime.Now;
                managers.CreatedBy = reportingManager.CreatedBy;
                managers.ModifiedOn = DateTime.Now;
                managers.ModifiedBy = reportingManager.ModifiedBy;
                managers.IsActive = reportingManager.IsActive;
            }
            var response = await context.SaveChangesAsync();
            return response == 1 ? true : false;
        }
    }
}
