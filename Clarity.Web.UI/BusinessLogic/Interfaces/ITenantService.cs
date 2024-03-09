using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface ITenantService
    {
        Task<bool> fnRegisterUserAsync(RegisterUser registerUser);
        Task<List<User>> fetchUsers();
    }
}
