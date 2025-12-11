using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IUserMailBoxService
    {
        Task<List<UserMailBox>> GetAllUserMailBoxAsync();

        Task<List<UserMailBox>> GetAllUserMailBoxAsync(long userId);

        Task InsertOrUpdateUserMailBox(UserMailBox mailBox);

        Task<bool> ReadUserMailBox(UserMailBox mailBox);

    }
}
