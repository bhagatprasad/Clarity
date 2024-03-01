using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IDesignationService
    {
        Task<bool> CreateDesignation(Designation designation);
        Task<List<Designation>> GetAllDesignation();
    }
}
