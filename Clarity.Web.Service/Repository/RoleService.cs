using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDBContext context;
        public RoleService(ApplicationDBContext _context)
        {
            this.context = _context;
        }

        public async Task<bool> DeleteRole(long roleId)
        {
            if (roleId > 1000)
            {
                var role = await context.roles.FindAsync(roleId);

                if (role == null)
                {
                    return false;
                }

                role.IsActive = false;
                role.ModifiedOn = DateTimeOffset.Now;
                role.ModifiedBy = -1;

                var responce = await context.SaveChangesAsync();

                return responce == 1 ? true : false;
            }
            return false;
        }

        public async Task<List<Roles>> fetchAllRoles()
        {
            var roles = await context.roles.Where(x => x.IsActive).ToListAsync();

            return roles;
        }

        public async Task<bool> InsertOrUpdateRole(Roles roles)
        {
            if (roles.Id > 0)
            {
                var role = await context.roles.FindAsync(roles.Id);

                if (role != null)
                {
                    role.Name = roles.Name;
                    role.Code = roles.Code;
                    role.ModifiedOn = roles.ModifiedOn;
                    role.ModifiedBy = roles.ModifiedBy;
                }
                else
                {
                    return false;
                }
            }
            else if (await checkRoleExistsOrNot(roles))
            {
                var role = await context.roles.Where(x => x.Name.ToLower().Trim() == roles.Name.ToLower().Trim() && !x.IsActive).FirstOrDefaultAsync();

                if (role != null)
                {
                    role.Code = roles.Code;
                    role.ModifiedOn = roles.ModifiedOn;
                    role.ModifiedBy = roles.ModifiedBy;
                    role.IsActive = true;
                }
            }
            else
            {
                await context.roles.AddAsync(roles);
            }

            var responce = await context.SaveChangesAsync();

            return responce == 1 ? true : false;
        }
        private async Task<bool> checkRoleExistsOrNot(Roles roles)
        {
            var exisits = await context.roles.AnyAsync(x => x.Name.ToLower().Trim() == roles.Name.ToLower().Trim() && !x.IsActive);

            return exisits;
        }
    }
}
