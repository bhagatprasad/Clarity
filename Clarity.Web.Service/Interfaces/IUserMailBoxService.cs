using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IUserMailBoxService
    {
        Task<UserMailBox> GetAllUserMailBoxAsync();
        Task<UserMailBox> GetUserMailBoxAsync(long userMailBoxID);
    }
}
