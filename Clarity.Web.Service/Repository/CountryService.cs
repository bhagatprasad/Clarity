using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Clarity.Web.Service.Repository
{
    public class CountryService : ICountryService
    {
        private readonly ApplicationDBContext context;
       
        public CountryService(ApplicationDBContext dbcontext)
        {
            this.context = dbcontext;
        }
        public async Task<List<Country>> fetchAllCountries()
        {
            var countries = await context.countries.Where(x => x.IsActive).ToListAsync();

            return countries;
        }

        public async Task<bool> InsertOrUpdateCountry(Country country)
        {
            if (country.Id > 0)
            {
                var _country = await context.countries.FindAsync(country.Id);

                if (_country != null)
                {
                    _country.Name = country.Name;
                    _country.Code = country.Code;
                    _country.ModifiedOn = country.ModifiedOn;
                    _country.ModifiedBy = country.ModifiedBy;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                await context.countries.AddAsync(country);
            }

            var responce = await context.SaveChangesAsync();

            return responce == 1 ? true : false;
        }
    }
}
