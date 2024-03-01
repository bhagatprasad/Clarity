using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface ICountryService
    {
        Task<List<Country>> fetchAllCountries();

        Task<bool> InsertOrUpdateCountry(Country country);
    }
}
