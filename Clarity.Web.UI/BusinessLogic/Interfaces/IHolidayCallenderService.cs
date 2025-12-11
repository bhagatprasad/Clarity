using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IHolidayCallenderService
    {
        Task<List<HolidayCallender>> fetchAllHolidayCallendersAsync();
        Task<List<HolidayCallender>> fetchAllHolidayCallendersAsync(int year);
        Task<bool> InsertOrUpdateHolidayCallenderAsync(HolidayCallender holidayCallender);
        Task<bool> RemoveHolidayCallenderAsync(long callenderId);
    }
}
