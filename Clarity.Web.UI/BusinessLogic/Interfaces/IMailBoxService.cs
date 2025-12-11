using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IMailBoxService
    {
        Task<List<MailBox>> GetMailBoxClinetAsync();
        Task<MailBox> InsertMailMessageForClientAsync(MailBox mailBox);

    }
}
