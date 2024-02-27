using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Helpers;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Clarity.Web.Service.Repository
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDBContext dbcontext;
        public string usedGenaratesTokenKey { get; }
        public AuthService(
            string usedGenaratesTokenKey,
            ApplicationDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
            this.usedGenaratesTokenKey = usedGenaratesTokenKey;
        }
        public async Task<AuthResponse> AuthenticateUser(string username, string password)
        {
            AuthResponse authResponse = new AuthResponse();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var dbUser = await dbcontext.users.Where(x => x.Email.ToLower().Trim() == username.ToLower().Trim()).FirstOrDefaultAsync();
                if (dbUser != null)
                {
                    var isValidUser = HashSalt.VerifyPassword(password, dbUser.PasswordHash, dbUser.PasswordSalt);

                    if (isValidUser)
                    {
                        var tokenhandler = new JwtSecurityTokenHandler();

                        var tokenkey = Encoding.ASCII.GetBytes(usedGenaratesTokenKey);

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                new Claim(ClaimTypes.Name, username)

                            }),
                            Expires = DateTime.UtcNow.AddHours(1),
                            SigningCredentials = new SigningCredentials(
                                new SymmetricSecurityKey(tokenkey),
                                SecurityAlgorithms.HmacSha256Signature)
                        };

                        var token = tokenhandler.CreateToken(tokenDescriptor);

                        var writtoken = tokenhandler.WriteToken(token);

                        authResponse = new AuthResponse { JwtToken = writtoken };
                        authResponse.ValidPassword = true;
                        authResponse.ValidUser = true;
                        authResponse.IsActive = dbUser.IsActive;
                        authResponse.StatusCode = string.Empty;
                        authResponse.StatusMessage = string.Empty;
                    }
                    else
                    {
                        authResponse.StatusMessage = "Invalid Password";
                        authResponse.ValidUser = true;
                        authResponse.ValidPassword = false;
                    }
                }

                else
                {
                    authResponse.StatusMessage = "Invalid user";

                    authResponse.ValidUser = false;
                }
            }
            return authResponse;
        }

        public async Task<ApplicationUser> GenarateUserClaims(AuthResponse authResponse)
        {
            try
            {
                var tokenkey = Encoding.ASCII.GetBytes(authResponse.JwtToken);
                var tokhand = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                var principle = tokhand.ValidateToken(usedGenaratesTokenKey,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(tokenkey),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    }, out securityToken);

                var jwttoken = securityToken as JwtSecurityToken;
                if (jwttoken != null && jwttoken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                {
                    throw new SecurityTokenException("token invalid");
                }
                else
                {
                    string Username = principle.Identity.Name;
                    User user = dbcontext.users.Where(x => (x.Email == Username || x.Phone == Username) && x.IsActive).FirstOrDefault();
                    if (user != null)
                    {
                        ApplicationUser app = new ApplicationUser();
                        app.Id = user.Id;
                        app.Email = user.Email;
                        app.FirstName = user.FirstName;
                        app.LastName = user.LastName;
                        app.Phone = user.Phone;
                        app.RoleId = user.RoleId;
                        return app;
                    }

                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
