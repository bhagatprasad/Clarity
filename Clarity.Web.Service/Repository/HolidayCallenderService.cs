using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class HolidayCallenderService : IHolidayCallenderService
    {
        private readonly ApplicationDBContext context;

        public HolidayCallenderService(ApplicationDBContext dbcontext)
        {
            this.context = dbcontext;
        }
        public async Task<List<HolidayCallender>> fetchAllHolidayCallendersAsync()
        {
            return await context.holidayCallenders.ToListAsync();
        }

        public async Task<List<HolidayCallender>> fetchAllHolidayCallendersAsync(int year)
        {
            return await context.holidayCallenders.Where(x => x.Year == year).ToListAsync();
        }

        public async Task<bool> InsertOrUpdateHolidayCallenderAsync(HolidayCallender holidayCallender)
        {
            if (holidayCallender.Id > 0)
            {
                var holiday = await context.holidayCallenders.FindAsync(holidayCallender.Id);
                if (holiday != null)
                {
                    holiday.FestivalName = holidayCallender.FestivalName;
                    holiday.HolidayDate = holidayCallender.HolidayDate;
                    holiday.Year = holidayCallender.Year;
                    holiday.ModifiedOn = holidayCallender.ModifiedOn;
                    holiday.ModifiedBy = holidayCallender.ModifiedBy;
                }
            }
            else
            {
                await context.holidayCallenders.AddAsync(holidayCallender);
            }
            var responce = await context.SaveChangesAsync();

            return responce == 1 ? true : false;
        }

        public async Task<bool> RemoveHolidayCallenderAsync(long callenderId)
        {
            var holiday = await context.holidayCallenders.FindAsync(callenderId);

            if (holiday != null)
                context.holidayCallenders.Remove(holiday);

            var responce = await context.SaveChangesAsync();
            return responce == 1 ? true : false;
        }
    }
}
