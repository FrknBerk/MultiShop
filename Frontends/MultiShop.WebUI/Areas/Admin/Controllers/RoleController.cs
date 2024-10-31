using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.IdentityDtos.RoleDtos;
using MultiShop.DtoLayer.IdentityDtos.UserRoleDtos;
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

        [Route("UserRoleCreate/{id}")]
        [HttpGet]
        public async Task<IActionResult> UserRoleCreate(string id)
        {
            ViewBag.Id = id;
            var values = await _roleIdentityService.GetRolesAsync();
            List<SelectListItem> roleValues = (from x in values
                                                   select new SelectListItem
                                                   {
                                                       Text = x.Name,
                                                       Value = x.Id
                                                   }).ToList();
            ViewBag.RoleValues = roleValues;
            var userRole = new CreateUserRoleDto
            {
                UserId = id
            };
            var createUserRoleDto = await _roleIdentityService.CreateUserRoleAsync(userRole);
            return View(createUserRoleDto);
        }

        [Route("UserRoleCreate/{id}")]
        [HttpPost]
        public async Task<IActionResult> UserRoleCreate(CreateUserRoleDto createUserRoleDto)
        {
            var result = await _roleIdentityService.CreateUserRoleAsync(createUserRoleDto);
            return RedirectToAction("UserList","User", new { area = "Admin" });
        }

        [Route("UserRoleDelete/{id}")]
        [HttpPost]
        public async Task<IActionResult> UserRoleDelete(CreateUserRoleDto createUserRoleDto)
        {
            var result = await _roleIdentityService.DeleteUserRoleAsync(createUserRoleDto);
            if (result)
            {
                return RedirectToAction("UserList", "User", new { area = "Admin" });
            }
            return View();
        }
    }
}
