using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Helpers;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class UserService : IUserService
    {
        private readonly ApplicationDBContext dbcontext;
        public UserService(ApplicationDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<User> fetchUser(long id)
        {
            return await dbcontext.users.FindAsync(id);
        }

        public async Task<List<User>> fetchUsers()
        {
            return await dbcontext.users.ToListAsync();
        }

        public async Task<bool> RegisterUser(RegisterUser registerUser)
        {
            if (registerUser.Id == 0 || registerUser.Id == null)
            {
                //create user 
                if (!string.IsNullOrEmpty(registerUser.Password))
                {
                    HashSalt hashSalt = HashSalt.GenerateSaltedHash(registerUser.Password);

                    User user = new User();
                    user.FirstName = registerUser.FirstName;
                    user.LastName = registerUser.LastName;
                    user.RoleId = registerUser.RoleId;
                    user.DepartmentId = registerUser.DepartmentId;
                    user.Email = registerUser.Email;
                    user.Phone = registerUser.Phone;
                    user.PasswordHash = hashSalt.Hash;
                    user.PasswordSalt = hashSalt.Salt;
                    user.CreatedBy = -1;
                    user.CreatedOn = DateTimeOffset.Now;
                    user.ModifiedBy = -1;
                    user.ModifiedOn = DateTimeOffset.Now;
                    user.IsActive = true;
                    user.UserWorngPasswordCount = 0;
                    user.IsBlocked = false;
                    await dbcontext.users.AddAsync(user);
                }
                else
                {
                    return false;
                }
            }
            else
            {

            }
            var responce = await dbcontext.SaveChangesAsync();

            return responce == 1 ? true : false;
        }
    }
}
