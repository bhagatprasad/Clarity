using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.BusinessLogic.Services;
using Clarity.Web.UI.Utility;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;

namespace Clarity.Web.UI
{
    public class Startup
    {
        public readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddMvc().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            var coreConfig = configuration.GetSection("CoreConfig");

            services.Configure<CoreConfig>(coreConfig);

            services.AddDirectoryBrowser();

            services.AddHttpContextAccessor();

            services.AddTransient<TokenAuthorizationHttpClientHandler>();

            services.AddHttpClient("AuthorizedClient")
                    .AddHttpMessageHandler<TokenAuthorizationHttpClientHandler>();

            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IDesignationService, DesignationService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<IClarityAuthenticationService, AuthenticationService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IRazorRendererHelper, RazorRendererHelper>();
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<IMonthlySalaryService, MonthlySalaryService>();
            services.AddScoped<IEmployeeSalaryService, EmployeeSalaryService>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/Forbidden/";
                options.Cookie.Name = "allowCookies";
                options.Cookie.IsEssential = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            services.AddNotyf(config =>
            {
                config.DurationInSeconds = 10;
                config.IsDismissable = true;
                config.Position = NotyfPosition.TopCenter;
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
                    ctx.Context.Response.Headers["Pragma"] = "no-cache";
                    ctx.Context.Response.Headers["Expires"] = "-1";

                }
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseNotyf();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
