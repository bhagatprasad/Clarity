using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Helpers;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class EmployeeSalaryService : IEmployeeSalaryService
    {
        private readonly ApplicationDBContext _dbContext;
        public EmployeeSalaryService(ApplicationDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<List<EmployeeSalaryModel>> FetchAllEmployeeSalarys()
        {
            return await FetchEmployeeSalaries();
        }

        public async Task<List<EmployeeSalaryModel>> FetchAllEmployeeSalarys(string employeeCode)
        {
            return await FetchEmployeeSalaries("", employeeCode);
        }

        public async Task<List<EmployeeSalaryModel>> FetchAllEmployeeSalarys(long employeeId)
        {
            return await FetchEmployeeSalaries("", "", "", "", employeeId);

        }

        public async Task<List<EmployeeSalaryModel>> FetchAllEmployeeSalarys(string month = "", string year = "")
        {
            return await FetchEmployeeSalaries("", "", month, year);

        }

        public async Task<EmployeeSalaryModel> FetchEmployeeSalary(long employeeSalaryId)
        {
            var employeeSalary = await FetchEmployeeSalaries("", "", "", "", 0, employeeSalaryId);
            return employeeSalary.FirstOrDefault();
        }

        public async Task<EmployeeSalary> InsertOrUpdateEmployeeSalaryAsync(EmployeeSalary employeeSalary)
        {
            if (employeeSalary.EmployeeSalaryId > 0)
            {
                var dbEmployeeSalary = await _dbContext.employeeSalaries.FindAsync(employeeSalary.EmployeeSalaryId);
                if (dbEmployeeSalary != null)
                {
                    bool hasChanges = EntityUpdater.HasChanges(dbEmployeeSalary, employeeSalary, nameof(EmployeeSalary.CreatedBy), nameof(EmployeeSalary.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(dbEmployeeSalary, employeeSalary, nameof(EmployeeSalary.CreatedBy), nameof(EmployeeSalary.CreatedOn));
                    }
                }
            }

            await _dbContext.SaveChangesAsync();

            return employeeSalary;
        }

        private async Task<List<EmployeeSalaryModel>> FetchEmployeeSalaries(string all = "", string employeeCode = "", string month = "", string year = "",
                                                                               long employeeId = 0, long employeeSalaryId = 0)
        {
            List<EmployeeSalaryModel> employeeSalaries = new List<EmployeeSalaryModel>();
            var employies = await _dbContext.employees.ToListAsync();
            var employeesSalaries = await _dbContext.employeeSalaries.ToListAsync();

            if (!string.IsNullOrEmpty(employeeCode))
            {
                employies = employies.Where(x => x.EmployeeCode == employeeCode).ToList();
            }
            else if (!string.IsNullOrEmpty(month) || !string.IsNullOrEmpty(year))
            {
                employeesSalaries = employeesSalaries.Where(x => x.SalaryMonth == month || x.SalaryYear == year).ToList();
            }
            else if (employeeId > 0)
            {
                employies = employies.Where(x => x.EmployeeId == employeeId).ToList();
                employeesSalaries = employeesSalaries.Where(x => x.EmployeeId == employeeId).ToList();
            }
            else if (employeeSalaryId > 0)
            {
                employeesSalaries = employeesSalaries.Where(x => x.EmployeeSalaryId == employeeSalaryId).ToList();
            }
            else
            {
                employies = employies;
                employeesSalaries = employeesSalaries;
            }
            employeeSalaries = (from salary in employeesSalaries
                                join employee in employies
                                on salary.EmployeeId equals employee.EmployeeId into employeejoin
                                from employeeinfo in employeejoin.DefaultIfEmpty()
                                select new EmployeeSalaryModel { employee = employeeinfo, employeeSalary = salary }).OrderByDescending(x=>x.employeeSalary.CreatedBy).ToList();

            return employeeSalaries;
        }
    }
}

