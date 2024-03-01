
using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<AddEditEmployee>> fetchAllEmployeesAsync();
        Task<bool> InsertOrUpdateAsync(AddEditEmployee employee);
        Task<AddEditEmployee> fetchEmployeeAsync(long employeeId);
    }
}
