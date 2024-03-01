using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class CityService : ICityService
    {
        private readonly ApplicationDBContext context;

        public CityService(ApplicationDBContext dbcontext)
        {
            this.context = dbcontext;
        }
        public async Task<List<City>> fetchAllCities()
        {
            return await context.cities.ToListAsync();
        }

        public async Task<bool> InsertOrUpdateCity(City city)
        {
            if (city.Id > 0)
            {
                var dbCity = await context.cities.FindAsync(city.Id);
                if (dbCity != null)
                {
                    dbCity.Name = city.Name;
                    dbCity.Code = city.Code;
                    dbCity.ModifiedOn = city.ModifiedOn;
                    dbCity.ModifiedBy = city.ModifiedBy;
                }
            }
            else
            {
                await context.cities.AddAsync(city);
            }
            var responce = await context.SaveChangesAsync();

            return responce == 1 ? true : false;
        }
    }
}
