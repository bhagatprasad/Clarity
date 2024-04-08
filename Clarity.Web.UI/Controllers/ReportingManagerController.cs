using AspNetCoreHero.ToastNotification.Abstractions;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.BusinessLogic.Services;
using Clarity.Web.UI.Models;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Admin")]
    public class ReportingManagerController : Controller
    {
        private readonly IReportingManagerService reportingManagerService;
        private readonly INotyfService notyfService;
        private static readonly ILog log = LogManager.GetLogger(typeof(ReportingManagerController));
        private readonly IEmployeeService employeeService;
        public ReportingManagerController(IReportingManagerService _reportingManagerService, INotyfService _notyfService, IEmployeeService employeeService)
        {
            this.reportingManagerService = _reportingManagerService;
            this.notyfService = _notyfService;
            this.employeeService = employeeService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> FetchAllReportingManager()
        {
            try
            {
                var repoManager = await reportingManagerService.FetchAllReportingManager();
                log.Info(JsonConvert.SerializeObject(repoManager));
                return Json(new { data = repoManager });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                log.Error("FetchAllReportingManager.." + ex);
                throw ex;
            }
        }

        [HttpPost]

        public async Task<IActionResult> AddEditReportingManager([FromBody] ReportingManager reportingManager)
        {
            try
            {
                if (reportingManager != null)
                {
                    bool response = false;
                    if (reportingManager.ManagerId > 0)
                        response = await reportingManagerService.UpdateReportingManager(reportingManager.EmployeeId, reportingManager);
                    else
                        response = await reportingManagerService.CreateReportingManager(reportingManager);
                    if (response)
                    {
                        if (reportingManager.ManagerId > 0)
                            notyfService.Success("Reporting Manager is updated successfully");
                        else
                            notyfService.Success("Reporting Manager is created successfully");

                        var employees = await employeeService.fetchAllEmployeesAsync();

                        return Json(new { data = employees });
                    }
                    notyfService.Warning("Something Went Wrong");

                    return Json(response);
                }
                notyfService.Warning("Something Went Wrong ");
                return Json(false);

            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                log.Error("InsertOrUpdateReportingManager.." + ex);
                throw ex;
            }
        }
    }
}
