using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.IdentityServer.Dtos.RoleDtos;
using MultiShop.IdentityServer.Dtos.UserRoleDtos;
using MultiShop.IdentityServer.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet("RoleList")]
        public IActionResult RoleList()
        {
            var values = _roleManager.Roles.ToList();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDto createRoleDto)
        {
            var value = new IdentityRole
            {
                Name = createRoleDto.Name
            };
            var roleExists = await _roleManager.RoleExistsAsync(createRoleDto.Name);
            
            if (!roleExists)
            {
                var result = await _roleManager.CreateAsync(value);
                if (result.Succeeded)
                    return Ok("Role ekleme işlemi başarılı");
                else
                    return Ok("Ekleme işlemi başarısız");
            }
            else
            {
                return Ok("Bu isimde rol var");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto updateRoleDto)
        {
            var value = new IdentityRole()
            {
                Id = updateRoleDto.Id,
                Name = updateRoleDto.Name,
                ConcurrencyStamp = updateRoleDto.ConcurrencyStamp
            };
            var result = await _roleManager.UpdateAsync(value);
            if (result.Succeeded)
            {
                return Ok("Role güncelleme işlemi başarılı");
            }
            else
            {
                return Ok("Güncelleme işlem Başarısız");
            }
        }

        [Route("DeleteRole")]
        [HttpPost]
        public async Task<IActionResult> DeleteRole(UpdateRoleDto updateRoleDto)
        {
            var value = new IdentityRole()
            {
                Id = updateRoleDto.Id,
                Name = updateRoleDto.Name,
                ConcurrencyStamp = updateRoleDto.ConcurrencyStamp
            };
            var result = await _roleManager.DeleteAsync(value);
            if(result.Succeeded)
            {
                return Ok("Role silme işlemi başarılı");
            }
            else
            {
                return Ok("Silme işlem Başarısız");
            }
        }

        [HttpGet("GetByIdRole")]
        public async Task<IActionResult> GetByIdRole(string id)
        {
            var result = await _roleManager.FindByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("GetUsersInRole")]
        public async Task<IActionResult> GetUsersInRoleAsync(string roleName)
        {
            var result = await _userManager.GetUsersInRoleAsync(roleName);
            return Ok(result);
        }

        [HttpGet("GetUserNameRole")]
        public async Task<IActionResult> GetUserNameRoleAsync(string userName)
        {
            var user = await _userManager.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            var userRoleName = await _userManager.GetRolesAsync(user);
            var role = await _roleManager.Roles.Where(x => x.Name == userRoleName.FirstOrDefault()).FirstOrDefaultAsync();
            if (role != null)
                return Ok(role.Name);
            return Ok(null);
        }

        [Route("CreateUserRole")]
        [HttpPost]
        public async Task<IActionResult> CreateUserRole(CreateUserRoleDto createUserRoleDto)
        {
            var user = await _userManager.Users.Where(x => x.Id == createUserRoleDto.UserId).FirstOrDefaultAsync();
            if(createUserRoleDto.RoleId != null)
            {
                var roleIdentity = await _roleManager.Roles.Where(x => x.Id == createUserRoleDto.RoleId).FirstOrDefaultAsync();
                var roleName = await _roleManager.GetRoleIdAsync(roleIdentity);
                var result = await _userManager.AddToRoleAsync(user, roleIdentity.Name);
                user.ExistUserRole = true;
                await _userManager.UpdateAsync(user);
                return Ok(createUserRoleDto);
            }
            var userRoleName = await _userManager.GetRolesAsync(user);
            var role = await _roleManager.Roles.Where(x =>x.Name == userRoleName.FirstOrDefault()).FirstOrDefaultAsync();
            var userRole = await _roleManager.GetRoleIdAsync(role);
            var ifUserRole = await _userManager.IsInRoleAsync(user, role.Name); // true dönüyor
            if (!ifUserRole)
            {
                var result = await _userManager.AddToRoleAsync(user, role.Name);//Tekrar ekleme işlemi yaptığında hata veriyor
                if (result.Succeeded)
                {
                    user.ExistUserRole = true;
                    await _userManager.UpdateAsync(user);
                }
            }
             
            //var result1 = await _userManager.GetRolesAsync(user); //Manager dönüyor
            //var result2 = await _userManager.GetUsersInRoleAsync(role.Name); //Burda kullanıcı bilgileri dönüyor
            var createUserRole = new CreateUserRoleDto
            {
                RoleId = role.Id,
                UserId = user.Id,
            };
            return Ok(createUserRole);
        }

        [Route("DeleteUserRole")]
        [HttpPost]
        public async Task<IActionResult> DeleteUserRole(CreateUserRoleDto createUserRoleDto)
        {
            var user = await _userManager.Users.Where(x => x.Id == createUserRoleDto.UserId).FirstOrDefaultAsync();
            user.ExistUserRole = false;
            var role = await _roleManager.Roles.Where(x => x.Id == createUserRoleDto.RoleId).FirstOrDefaultAsync();
            var result = await _userManager.RemoveFromRoleAsync(user,role.Name);
            if (result.Succeeded)
            {
                await _userManager.UpdateAsync(user);
                return Ok("Kullanıcı başarıyla silindi");
            }
            else
                return BadRequest("Kullanıcı silinemedi");
        }
    }
}
