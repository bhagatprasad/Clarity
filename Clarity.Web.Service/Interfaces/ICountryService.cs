using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface ICountryService
    {
        Task<List<Country>> fetchAllCountries();

        Task<bool> InsertOrUpdateCountry(Country country);

        //Task<bool> DeleteCountry(long countryId);
    }
}
