using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class MailBoxService : IMailBoxService
    {
        private readonly ApplicationDBContext context;
        private readonly IUserMailBoxService userMailBoxService;
        public MailBoxService(ApplicationDBContext injectedContext, IUserMailBoxService userMailBoxService)
        {
            this.context = injectedContext;
            this.userMailBoxService = userMailBoxService;
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


                //get all users from database

                var users = await context.users.ToListAsync();

                UserMailBox userMailBox = null;

                if (mailBox.IsForAll)
                {
                    foreach (var item in users)
                    {
                        userMailBox = new UserMailBox()
                        {
                            CreatedBy = mailBox.CreatedBy,
                            CreatedOn = mailBox.CreatedOn,
                            IsActive = mailBox.IsActive,
                            IsRead = false,
                            MailBoxId = mailBox.MailBoxId,
                            ModifiedBy = mailBox.ModifiedBy,
                            ModifiedOn = mailBox.ModifiedOn,
                            UserId = item.Id
                        };

                        await userMailBoxService.InsertOrUpdateUserMailBox(userMailBox);
                    }
                }
                else
                {
                    //do something w r t user
                }


                return mailBox;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
