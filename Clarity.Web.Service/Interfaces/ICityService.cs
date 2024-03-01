using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface ICityService
    {
        Task<List<City>> fetchAllCities();
        Task<bool> InsertOrUpdateCity(City city);
    }
}
