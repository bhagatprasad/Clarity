using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IDesignationService
    {
        Task<bool> CreateDesignation(Designation designation);
        Task<bool> DeleteDesignation(long designationId);
        Task<bool> UpdateDesignation(long designationId, Designation designation);
        Task<Designation> GetDesignation(long designationId);
        Task<List<Designation>> GetAllDesignation();
        Task<bool> VerifyDesignationAlreadyExists(string designation);
    }
}
