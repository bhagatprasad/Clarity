using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> fetchUsers();
        Task<User> fetchUser(long id);
        Task<bool> RegisterUser(RegisterUser registerUser);
    }
}
