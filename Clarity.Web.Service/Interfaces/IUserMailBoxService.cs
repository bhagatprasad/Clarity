using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IUserMailBoxService
    {
        Task<List<UserMailBox>> GetAllUserMailBoxAsync();

        Task<List<UserMailBox>> GetAllUserMailBoxAsync(long userId);

        Task<bool> ReadUserMailBox(UserMailBox mailBox);

    }
}
