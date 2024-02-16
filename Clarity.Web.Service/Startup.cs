using Clarity.Web.Service.DBConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

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
            services.AddDbContext<ApplicationDBContext>(
            options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),

            sqlServerOptionsAction: sqlOptions =>
            {
            sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Maximum number of retries
            maxRetryDelay: TimeSpan.FromSeconds(30), // Maximum delay between retries
            errorNumbersToAdd: null); // SQL error codes to retry on (you can specify error codes if needed)
            }));

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddMvc().AddXmlSerializerFormatters();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                     builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithMethods("GET", "PUT", "DELETE", "POST", "PATCH") //not really necessary when AllowAnyMethods is used.
            );
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Clarity API",
                    Description = "Clarity Fashion Business API",
                    TermsOfService = new Uri("https://Clarity.com"),
                    Contact = new OpenApiContact
                    {
                        Name = "Durga Prasad Baghuthu",
                        Email = "dbaghuthu@betalen.in",
                        Url = new Uri("https://twitter.com/Clarityfashion"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Clarity Open License",
                        Url = new Uri("https://Clarity.com"),
                    }
                });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
            app.UseStaticFiles();

            app.UseSwagger();

            // This middleware serves the Swagger documentation UI;
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Smaya Global API Services V1");
                //c.RoutePrefix = string.Empty;
            });
        }

    }
}
