using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface ICityService
    {
        Task<List<City>> fetchAllCities();
        Task<bool> InsertOrUpdateCity(City city);
    }
}
