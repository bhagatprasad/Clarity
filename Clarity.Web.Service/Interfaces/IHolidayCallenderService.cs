using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IHolidayCallenderService
    {
        Task<List<HolidayCallender>> fetchAllHolidayCallendersAsync();
        Task<List<HolidayCallender>> fetchAllHolidayCallendersAsync(int year);
        Task<bool> InsertOrUpdateHolidayCallenderAsync(HolidayCallender holidayCallender);
        Task<bool> RemoveHolidayCallenderAsync(long callenderId);
    }
}
