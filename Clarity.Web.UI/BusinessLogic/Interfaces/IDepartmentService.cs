using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IDepartmentService
    {
        Task<bool> CreateDepartment(Department department);
        Task<bool> DeleteDepartment(long departmentId);
        Task<bool> UpdateDepartment(long departmentId, Department department);
        Task<Department> GetDepartment(long departmentId);
        Task<List<Department>> GetAllDepartment();
        Task<bool> VerifyDepartmentAlreadyExists(string department);
    }
}
