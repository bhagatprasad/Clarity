using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDBContext dbcontext;
        public DepartmentService(ApplicationDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<bool> CreateDepartment(Department department)
        {
            if (department != null)
                await dbcontext.department.AddAsync(department);
            var response = await dbcontext.SaveChangesAsync();
            return response == 1 ? true : false;

        }

        public async Task<bool> DeleteDepartment(long departmentId)
        {
            var department = await dbcontext.department.FindAsync(departmentId);
            if (department != null)
            {
                dbcontext.department.Remove(department);
            }
            var response = await dbcontext.SaveChangesAsync();
            return response == 1 ? true : false;
        }

        public async Task<List<Department>> GetAllDepartment()
        {
            return await dbcontext.department.Where(x => x.IsActive).ToListAsync();
        }

        public async Task<Department> GetDepartment(long departmentId)
        {
            var department = await dbcontext.department.FindAsync(departmentId);
            if (department != null)
            {
                return department;
            }
            return null;
        }

        public async Task<bool> UpdateDepartment(long departmentId, Department _department)
        {
            var department = await dbcontext.department.FindAsync(departmentId);
            if (department != null)
            {
                department.Name = _department.Name;
                department.Description = _department.Description;
                department.Code = _department.Code;
                department.CreatedOn = DateTime.Now;
                department.CreatedBy = _department.CreatedBy;
                department.ModifiedOn = DateTime.Now;
                department.ModifiedBy = _department.ModifiedBy;
                department.IsActive = _department.IsActive;
            }
            var response = await dbcontext.SaveChangesAsync();
            return response == 1 ? true : false;
        }
        public async Task<bool> VerifyDepartmentAlreadyExists(string department)
        {
            return await dbcontext.department.AnyAsync(x => x.Name.ToLower().Trim() == department.ToLower().Trim());
        }
    }
}
