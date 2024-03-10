using AspNetCoreHero.ToastNotification.Abstractions;
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
        private readonly INotyfService _notyfService;
        public RolesController(IRolesService rolesService,
            INotyfService _notyfService)
        {
            this.rolesService = rolesService;
            this._notyfService = _notyfService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var responce = await rolesService.fetchAllRoles();

            return View(responce);
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
                    _notyfService.Success("Role was created successfully");
                    return RedirectToAction("Index", "Roles", null);
                }
            }

            ModelState.AddModelError("", "Something went wrong ,please fix and submit again");
            _notyfService.Error("Something went wrong ,please fix and submit again");
            return View(role);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var responce = await rolesService.fetchRole(id);

            if (responce != null)
            {
                return View(responce);
            }

            _notyfService.Error("Something went wrong");

            return RedirectToAction("Index", "Roles", null);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Roles role)
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
                    _notyfService.Success("Role was updated successfully");
                    return RedirectToAction("Index", "Roles", null);
                }
            }

            ModelState.AddModelError("", "Something went wrong ,please fix and submit again");

            _notyfService.Error("Something went wrong ,please fix and submit again");

            return View(role);
        }

        [HttpGet]
        public async Task<IActionResult> LoadRoles()
        {
            var roles = await rolesService.fetchAllRoles();
            return Json(new { data = roles });

        }
    }
}
