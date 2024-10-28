using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.RoleDtos;
using MultiShop.WebUI.Services.IdentityServices.RoleIdentityServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Role")]
    public class RoleController : Controller
    {
        private readonly IRoleIdentityService _roleIdentityService;

        public RoleController(IRoleIdentityService roleIdentityService)
        {
            _roleIdentityService = roleIdentityService;
        }

        [Route("RoleList")]
        public async Task<IActionResult> RoleList()
        {
            var values = await _roleIdentityService.GetRolesAsync();
            return View(values);
        }

        [Route("CreateRole")]
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [Route("CreateRole")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDto createRoleDto)
        {
            var result = await _roleIdentityService.CreateRoleAsync(createRoleDto);
            if (result == true)
                return RedirectToAction("RoleList", "Role", new { area = "Admin" });
            else
                return View();
        }

        [Route("UpdateRole/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateRole(string id)
        {
            var value = await _roleIdentityService.GetByIdRoleAsync(id);
            return View(value);
        }

        [Route("UpdateRole/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto updateRoleDto)
        {
            var result = await _roleIdentityService.UpdateRoleAsync(updateRoleDto);
            if (result == true)
                return RedirectToAction("RoleList", "Role", new { area = "Admin" });
            else
                return View();
        }

        [Route("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var value = await _roleIdentityService.GetByIdRoleAsync(id);
            var result = await _roleIdentityService.DeleteRoleAsync(value);
            if (result == true)
                return RedirectToAction("RoleList", "Role", new { area = "Admin" });
            else
                return View();
        }
    }
}
