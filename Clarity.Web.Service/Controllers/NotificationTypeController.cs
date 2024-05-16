using Clarity.Web.Service.Helpers;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Clarity.Web.Service.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ClarityAuthorize]
    public class NotificationTypeController : ControllerBase
    {
        private readonly INotificationTypeService notificationTypeService;
        public NotificationTypeController(INotificationTypeService _notificationTypeService) 
        {
            this.notificationTypeService = _notificationTypeService;
        }

        [HttpGet]
        [Route("FetchAllNotificationType")]
        public async Task<IActionResult> FetchAllNotificationType()
        {
            try
            {
                var notifications = await notificationTypeService.FetchAllNotificationType();
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Getting NotificationType", 500, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateNotificationType")]
        public async Task<ActionResult> InsertOrUpdateNotificationType(NotificationType notificationType)
        {
            try
            {
                var responce = await notificationTypeService.InsertOrUpdateNotificationType(notificationType);

                return Ok(responce);
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Saving or Updating NotificationType", 500, ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteNotificationType/{notificationTypeid}")]
        public async Task<IActionResult> DeleteNotificationType(long notificationTypeid)
        {
            try
            {
                var responce = await notificationTypeService.DeleteNotificationType(notificationTypeid);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Deleting NotificationType", 500, ex.Message);
            }
        }

        [HttpGet]
        [Route("fetchNotificationType/{notificationTypeid}")]
        public async Task<IActionResult> fetchNotificationType(long notificationTypeid)
        {
            try
            {
                var responce = await notificationTypeService.FetchNotificationType(notificationTypeid);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                throw new HttpRequestExceptionMessage("Error Getting  NotificationType by Id ", 500, ex.Message);

            }
        }
    }
}
