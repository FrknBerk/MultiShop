using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.RoleDtos;
using MultiShop.DtoLayer.IdentityDtos.UserDtos;
using MultiShop.DtoLayer.IdentityDtos.UserRoleDtos;

namespace MultiShop.WebUI.Services.IdentityServices.RoleIdentityServices
{
    public interface IRoleIdentityService
    {
        Task<List<ResultRoleDto>> GetRolesAsync();
        Task<bool> CreateRoleAsync(CreateRoleDto createRoleDto);
        Task<bool> UpdateRoleAsync(UpdateRoleDto updateRoleDto);
        Task<bool> DeleteRoleAsync(UpdateRoleDto updateRoleDto);
        Task<UpdateRoleDto> GetByIdRoleAsync(string id);
        Task<CreateUserRoleDto> CreateUserRoleAsync(CreateUserRoleDto createUserRoleDto);
        Task<bool> DeleteUserRoleAsync(CreateUserRoleDto createUserRoleDto);
        Task<List<ResultUserDto>> GetUsersInRoleAsync(string roleName);
        Task<ResultRoleDto> GetRoleNameAsync(string roleName);

	}
}
