using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class EmployeeSalaryStructureService : IEmployeeSalaryStructureService
    {
        private readonly ApplicationDBContext dbcontext;
        public EmployeeSalaryStructureService(ApplicationDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<EmployeeSalaryStructure> fetchEmployeeSalaryStructure(long employeeId)
        {
            return await dbcontext.employeeSalaryStructures.Where(x => x.IsActive == true && x.EmployeeId == employeeId).FirstOrDefaultAsync();
        }

        public async Task<List<EmployeeSalaryStructure>> fetchEmployeeSalaryStructuresAsync()
        {
            return await dbcontext.employeeSalaryStructures.Where(x => x.IsActive == true).ToListAsync();
        }
    }
}
