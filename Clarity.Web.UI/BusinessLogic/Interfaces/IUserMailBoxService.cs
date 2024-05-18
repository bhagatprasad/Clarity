using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IUserMailBoxService
    {
        Task<List<UserMailBox>> GetAllUserMailBoxAsync();

        Task<List<UserMailBox>> GetAllUserMailBoxAsync(long userId);

        Task<bool> ReadUserMailBox(UserMailBox mailBox);
    }
}
