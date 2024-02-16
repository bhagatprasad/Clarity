using Clarity.Web.Service.DBConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services) 
        { 
        }
    }
}
