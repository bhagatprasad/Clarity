using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IMailBoxService
    {
        Task<List<MailBox>> GetMailBoxesAsync();

        Task<MailBox> InsertMailMessageAsync(MailBox mailBox);

    }
}
