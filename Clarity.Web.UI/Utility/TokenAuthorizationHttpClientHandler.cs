using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace Clarity.Web.UI.Utility
{
    public class TokenAuthorizationHttpClientHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CoreConfig _coreConfig;


        public TokenAuthorizationHttpClientHandler(IHttpContextAccessor httpContextAccessor, IOptions<CoreConfig> coreConfig)
        {
            _httpContextAccessor = httpContextAccessor;
            _coreConfig = coreConfig.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var accessToken = _httpContextAccessor.HttpContext.Session.GetString("AccessToken");

            request.Headers.Add("Authorization", accessToken);

         
            return await base.SendAsync(request, cancellationToken);
        }
    }

}
