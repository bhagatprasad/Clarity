using Clarity.Web.UI.Models;
using System.Data;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IRolesService
    {
        Task<List<Roles>> fetchAllRoles();

        Task<bool> InsertOrUpdateRole(Roles roles);

        Task<bool> DeleteRole(long roleId);
    }
}
