

using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace Clarity.Web.Service.Helpers
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ClarityAuthorize : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext != null)
            {
                Microsoft.Extensions.Primitives.StringValues authTokens;

                filterContext.HttpContext.Request.Headers.TryGetValue("Authorization", out authTokens);

                var _token = authTokens.FirstOrDefault();

                if (_token != null)
                {
                    string authToken = _token;

                    if (authToken != null)
                    {
                        if (IsValidToken(authToken))
                        {
                            filterContext.HttpContext.Response.Headers.Add("Authorization", authToken);
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");
                            filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");
                            return;
                        }
                        else
                        {
                            filterContext.HttpContext.Response.Headers.Add("Authorization", authToken);
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "UnAuthorized");
                            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                            filterContext.Result = new JsonResult("NotAuthorized")
                            {
                                Value = new
                                {
                                    Status = "Error",
                                    Message = "Invalid Token"
                                },
                            };
                        }

                    }

                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Please Provide Authorization";
                    filterContext.Result = new JsonResult("Please Provide Authorization")
                    {
                        Value = new
                        {
                            Status = "Error",
                            Message = "Please Provide authToken"
                        },
                    };
                }
            }
        }
        public bool IsValidToken(string authToken)
        {
            return CheckTokenIsValid(authToken);
        }
        public bool CheckTokenIsValid(string token)
        {
            var tokenTicks = GetTokenExpirationTime(token);

            var tokenDate = DateTimeOffset.FromUnixTimeSeconds(tokenTicks).UtcDateTime;

            var now = DateTime.Now.ToUniversalTime();

            var valid = tokenDate >= now;

            return valid;
        }
        public long GetTokenExpirationTime(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var tokenExp = jwtSecurityToken.Claims.First(claim => claim.Type.Equals("exp")).Value;
            var ticks = long.Parse(tokenExp);
            return ticks;
        }
    }
}
