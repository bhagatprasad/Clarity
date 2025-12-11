using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class UserMailBoxService : IUserMailBoxService
    {
        private readonly ApplicationDBContext context;

        public UserMailBoxService(ApplicationDBContext dbcontext)
        {
            this.context = dbcontext;
        }
        public async Task<List<UserMailBox>> GetAllUserMailBoxAsync()
        {

            return await context.userMailBoxes.Include(x => x.mailBox).ThenInclude(x => x.messageType).ToListAsync();
        }

        public async Task<List<UserMailBox>> GetAllUserMailBoxAsync(long userId)
        {
            return await context.userMailBoxes.Where(x => x.UserId == userId).Include(x => x.mailBox).ThenInclude(x => x.messageType).ToListAsync();
        }

        public async Task InsertOrUpdateUserMailBox(UserMailBox mailBox)
        {
            await context.userMailBoxes.AddAsync(mailBox);
            await context.SaveChangesAsync();
        }

        public async Task<bool> ReadUserMailBox(UserMailBox mailBox)
        {
            var userMailbox = await context.userMailBoxes.FindAsync(mailBox.UserMailBoxId);

            if(userMailbox != null)
            {
                userMailbox.ReadOn = mailBox.ReadOn;
                userMailbox.IsRead = true;
                userMailbox.ModifiedOn=mailBox.ModifiedOn;
                userMailbox.ModifiedBy = mailBox.ModifiedBy;

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
