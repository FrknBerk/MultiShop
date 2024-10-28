using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.RoleDtos;

namespace MultiShop.WebUI.Services.IdentityServices.RoleIdentityServices
{
    public interface IRoleIdentityService
    {
        Task<List<ResultRoleDto>> GetRolesAsync();
        Task<bool> CreateRoleAsync(CreateRoleDto createRoleDto);
        Task<bool> UpdateRoleAsync(UpdateRoleDto updateRoleDto);
        Task<bool> DeleteRoleAsync(UpdateRoleDto updateRoleDto);
        Task<UpdateRoleDto> GetByIdRoleAsync(string id);
    }
}
