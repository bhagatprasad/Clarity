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
using System.Reflection;
using log4net;
using log4net.Config;

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
            services.AddScoped<HttpClientService>();
            services.AddTransient<TokenAuthorizationHttpClientHandler>();
            services.AddHttpClient("AuthorizedClient").AddHttpMessageHandler<TokenAuthorizationHttpClientHandler>();
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
            services.AddScoped<IChangePasswordService, ChangePasswordService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IEmployeeSalaryStructureService, EmployeeSalaryStructureService>();
            services.AddScoped<IReportingManagerService, ReportingManagerService>();
            services.AddScoped<ITaskItemService, TaskItemService>();
            services.AddScoped<IHolidayCallenderService, HolidayCallenderService>(); 
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
                options.AccessDeniedPath = "/Unauthorized/Forbidden";
                options.Cookie.Name = "allowCookies";
                options.Cookie.IsEssential = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", policy =>
                    policy.RequireRole("Administrator"));

                options.AddPolicy("Admin", policy =>
                    policy.RequireRole("Admin"));

                options.AddPolicy("test", policy =>
                    policy.RequireRole("test"));

                options.AddPolicy("Software Engineer/Developer", policy =>
                    policy.RequireRole("Software Engineer/Developer"));

                options.AddPolicy("Quality Assurance Engineer/Tester", policy =>
                    policy.RequireRole("Quality Assurance Engineer/Tester"));

                options.AddPolicy("Project Manager", policy =>
                    policy.RequireRole("Project Manager"));

                options.AddPolicy("ProductManager", policy =>
                    policy.RequireRole("Product Manager"));

                options.AddPolicy("UXUIDesigner", policy =>
                    policy.RequireRole("UX/UI Designer"));

                options.AddPolicy("DataScientist", policy =>
                    policy.RequireRole("Data Scientist"));

                options.AddPolicy("DevOpsEngineer", policy =>
                    policy.RequireRole("DevOps Engineer"));

                options.AddPolicy("TechnicalSupportEngineer", policy =>
                    policy.RequireRole("Technical Support Engineer"));

                options.AddPolicy("TechnicalWriterDocumentationSpecialist", policy =>
                    policy.RequireRole("Technical Writer/Documentation Specialist"));

                options.AddPolicy("BusinessAnalyst", policy =>
                    policy.RequireRole("Business Analyst"));

                options.AddPolicy("ScrumMaster", policy =>
                    policy.RequireRole("Scrum Master"));

                options.AddPolicy("SystemAdministrator", policy =>
                    policy.RequireRole("System Administrator"));

                options.AddPolicy("NetworkEngineer", policy =>
                    policy.RequireRole("Network Engineer"));

                options.AddPolicy("SecurityEngineer", policy =>
                    policy.RequireRole("Security Engineer"));

                options.AddPolicy("ReleaseManager", policy =>
                    policy.RequireRole("Release Manager"));

                options.AddPolicy("DatabaseAdministrator", policy =>
                    policy.RequireRole("Database Administrator"));

                options.AddPolicy("SoftwareArchitect", policy =>
                    policy.RequireRole("Software Architect"));

                options.AddPolicy("FrontEndDeveloper", policy =>
                    policy.RequireRole("Front-end Developer"));

                options.AddPolicy("BackEndDeveloper", policy =>
                    policy.RequireRole("Back-end Developer"));

                options.AddPolicy("Full-stack Developer", policy =>
                    policy.RequireRole("Full-stack Developer"));

                options.AddPolicy("MobileApplicationDeveloper", policy =>
                    policy.RequireRole("Mobile Application Developer"));

                options.AddPolicy("AIMachineLearningEngineer", policy =>
                    policy.RequireRole("AI/Machine Learning Engineer"));

                options.AddPolicy("CloudEngineer", policy =>
                    policy.RequireRole("Cloud Engineer"));

                options.AddPolicy("AutomationEngineer", policy =>
                    policy.RequireRole("Automation Engineer"));

                options.AddPolicy("ITSupportSpecialist", policy =>
                    policy.RequireRole("IT Support Specialist"));

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
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseExceptionHandlerMiddleware();

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
