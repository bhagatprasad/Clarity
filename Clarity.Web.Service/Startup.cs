using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using System.Text;

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
               maxRetryCount: 5,
               maxRetryDelay: TimeSpan.FromSeconds(30), 
               errorNumbersToAdd: null); 
            }));

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddMvc().AddXmlSerializerFormatters();

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDesignationService,DesignationService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICityService, CityService>();
            services.AddCors(options =>
            {
                      options.AddPolicy("CorsPolicy",
                                         builder => builder
                                        .AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .WithMethods("GET", "PUT", "DELETE", "POST", "PATCH") 
                                        );
            });

            var usedGenaratesTokenKey = _configuration.GetValue<string>("UsedGenaratesTokenKey");

           

            services.AddScoped<IAuthService>
                (x => new AuthService
                (usedGenaratesTokenKey, x.GetRequiredService<ApplicationDBContext>()));


            var key = Encoding.ASCII.GetBytes(usedGenaratesTokenKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Clarity API",
                    Description = "Clarity Business API",
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
                c.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter the Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Authorization"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Smaya Global API Services V1");
            });
        }

    }
}
