using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IRoleService
    {
        Task<List<Roles>> fetchAllRoles();

        Task<bool> InsertOrUpdateRole(Roles roles);

        Task<bool> DeleteRole(long roleId);
    }
}
