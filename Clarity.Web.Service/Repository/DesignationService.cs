using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class DesignationService : IDesignationService
    {
        private readonly ApplicationDBContext dbcontext;
        public DesignationService(ApplicationDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

       

        public async Task<bool> CreateDesignation(Designation designation)
        {
            if (designation != null)
                await dbcontext.designation.AddAsync(designation);
            var response = await dbcontext.SaveChangesAsync();
            return response == 1 ? true : false;
        }

        public async Task<bool> DeleteDesignation(long designationId)
        {
            var designation = await dbcontext.designation.FindAsync(designationId);
            if (designation != null)
            {
                dbcontext.designation.Remove(designation);
            }
            var response = await dbcontext.SaveChangesAsync();
            return response == 1 ? true : false;
        }

        public async Task<List<Designation>> GetAllDesignation()
        {
            return await dbcontext.designation.Where(x => x.IsActive).ToListAsync();
        }

        public async Task<Designation> GetDesignation(long designationId)
        {
            var designation = await dbcontext.designation.FindAsync(designationId);
            if (designation != null)
            {
                return designation;
            }
            return null;
        }

        public async Task<bool> UpdateDesignation(long designationId, Designation _designation)
        {
            var designation = await dbcontext.designation.FindAsync(designationId);
            if (designation != null)
            {
                designation.Name = _designation.Name;
                designation.Code = _designation.Code;
                designation.CreatedOn = DateTime.Now;
                designation.CreatedBy = _designation.CreatedBy;
                designation.ModifiedOn = DateTime.Now;
                designation.ModifiedBy = _designation.ModifiedBy;
                designation.IsActive = _designation.IsActive;
            }
            var response = await dbcontext.SaveChangesAsync();
            return response == 1 ? true : false;
        }

        public async Task<bool> VerifyDesignationAlreadyExists(string designation)
        {
            return await dbcontext.designation.AnyAsync(x => x.Name.ToLower().Trim() == designation.ToLower().Trim());
        }
    }
}
