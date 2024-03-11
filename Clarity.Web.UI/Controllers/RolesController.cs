using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Admin")]
    public class RolesController : Controller
    {
        private readonly IRolesService rolesService;
        private readonly INotyfService notyfService;
        public RolesController(IRolesService rolesService,
            INotyfService notyfService)
        {
            this.rolesService = rolesService;
            this.notyfService = notyfService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var responce = await rolesService.fetchAllRoles();

                return View(responce);
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Roles role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    role.CreatedBy = -1;
                    role.CreatedOn = DateTimeOffset.Now;
                    role.ModifiedBy = -1;
                    role.ModifiedOn = DateTimeOffset.Now;
                    role.IsActive = true;

                    var responce = await rolesService.InsertOrUpdateRole(role);
                    if (responce)
                    {
                        notyfService.Success("Role was created successfully");
                        return RedirectToAction("Index", "Roles", null);
                    }
                }

                ModelState.AddModelError("", "Something went wrong ,please fix and submit again");
                notyfService.Error("Something went wrong ,please fix and submit again");
                return View(role);
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            try
            {
                var responce = await rolesService.fetchRole(id);

                if (responce != null)
                {
                    return View(responce);
                }

                notyfService.Error("Something went wrong");

                return RedirectToAction("Index", "Roles", null);
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Roles role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    role.CreatedBy = -1;
                    role.CreatedOn = DateTimeOffset.Now;
                    role.ModifiedBy = -1;
                    role.ModifiedOn = DateTimeOffset.Now;
                    role.IsActive = true;

                    var responce = await rolesService.InsertOrUpdateRole(role);

                    if (responce)
                    {
                        notyfService.Success("Role was updated successfully");
                        return RedirectToAction("Index", "Roles", null);
                    }
                }

                ModelState.AddModelError("", "Something went wrong ,please fix and submit again");

                notyfService.Error("Something went wrong ,please fix and submit again");

                return View(role);
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> LoadRoles()
        {
            try
            {
                var roles = await rolesService.fetchAllRoles();
                return Json(new { data = roles });
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
