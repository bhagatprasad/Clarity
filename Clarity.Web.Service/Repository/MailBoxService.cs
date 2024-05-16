using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class MailBoxService : IMailBoxService
    {
        private readonly ApplicationDBContext context;
        public MailBoxService(ApplicationDBContext injectedContext)
        {
            this.context = injectedContext;
        }
        public async Task<List<MailBox>> GetMailBoxesAsync()
        {
            try
            {
                var responce = await context.mailBoxes.Include(x => x.messageType).ToListAsync();

                return responce;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<MailBox> InsertMailMessageAsync(MailBox mailBox)
        {
            try
            {
                await context.mailBoxes.AddAsync(mailBox);
                await context.SaveChangesAsync();
                return mailBox;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
