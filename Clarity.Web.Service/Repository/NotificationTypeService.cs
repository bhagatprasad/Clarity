using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class NotificationTypeService : INotificationTypeService
    {
        private readonly ApplicationDBContext context;

        public NotificationTypeService(ApplicationDBContext _context)
        {
            this.context = _context;
        }
        public async Task<bool> DeleteNotificationType(long notificationTypeId)
        {
            if (notificationTypeId != 0)
            {
                var notificationTypess = await context.notificationTypes.FindAsync(notificationTypeId);
                if (notificationTypess == null)
                {
                    return false;
                }
                notificationTypess.IsActive = false;
                notificationTypess.ModifiedOn = DateTimeOffset.Now;
                notificationTypess.ModifiedBy = -1;

                var responce = await context.SaveChangesAsync();
                return responce == 1 ? true : false;
            }
            return false;
        }

        public async Task<List<NotificationType>> FetchAllNotificationType()
        {
            var notifications = await context.notificationTypes.Where(x => x.IsActive).ToListAsync();
            return notifications;
        }

        public async Task<NotificationType> FetchNotificationType(long notificationTypeId)
        {
            return await context.notificationTypes.Where(x => x.NotificationTypeId == notificationTypeId).FirstOrDefaultAsync();
        }

        public async Task<bool> InsertOrUpdateNotificationType(NotificationType notificationType)
        {
            if (notificationType.NotificationTypeId > 0)
            {
                var notificationTypess= await context.notificationTypes.Where(x => x.Name.ToLower().Trim() == notificationType.Name.ToLower().Trim() && !x.IsActive).FirstOrDefaultAsync();
                if (notificationTypess != null)
                {
                    notificationTypess.Description = notificationType.Description;
                    notificationTypess.ModifiedOn = notificationType.ModifiedOn;
                    notificationTypess.ModifiedBy = notificationType.ModifiedBy;
                    notificationTypess.IsActive = true;
                }
            }
            else
            {
                await context.notificationTypes.AddAsync(notificationType);
            }

            var response = await context.SaveChangesAsync();
            return response == 1 ? true : false;
        }
    }
}
