using Clarity.Web.Service.Models;
using System.Diagnostics.Eventing.Reader;

namespace Clarity.Web.Service.Interfaces
{
    public interface INotificationTypeService
    {
        Task<List<NotificationType>> FetchAllNotificationType();
        Task<bool> InsertOrUpdateNotificationType(NotificationType notificationType);
        Task<bool> DeleteNotificationType(long notificationTypeId);
        Task<NotificationType> FetchNotificationType(long notificationTypeId);
    }
}
