using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.Dtos.RoleDtos;
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

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
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
    }
}
