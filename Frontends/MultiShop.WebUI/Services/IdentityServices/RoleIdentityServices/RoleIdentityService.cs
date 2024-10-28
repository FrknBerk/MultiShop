using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.RoleDtos;
using MultiShop.DtoLayer.IdentityDtos.UserDtos;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MultiShop.WebUI.Services.IdentityServices.RoleIdentityServices
{
    public class RoleIdentityService : IRoleIdentityService
    {
        private readonly HttpClient _httpClient;

        public RoleIdentityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateRoleAsync(CreateRoleDto createRoleDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreateRoleDto>("api/Roles", createRoleDto);
            if (result.IsSuccessStatusCode == true)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteRoleAsync(UpdateRoleDto updateRoleDto)
        {
            var result = await _httpClient.PostAsJsonAsync<UpdateRoleDto>("api/Roles/DeleteRole",updateRoleDto);
            if (result.IsSuccessStatusCode == true)
                return true;
            else
                return false;
        }

        public async Task<UpdateRoleDto> GetByIdRoleAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("api/Roles/GetByIdRole?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateRoleDto>(jsonData);
            return values;
        }

        public async Task<List<ResultRoleDto>> GetRolesAsync()
        {
            var responseMessage = await _httpClient.GetAsync("api/Roles/RoleList");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultRoleDto>>(jsonData);
            return values;
        }

        public async Task<bool> UpdateRoleAsync(UpdateRoleDto updateRoleDto)
        {
            var result = await _httpClient.PutAsJsonAsync<UpdateRoleDto>("api/Roles", updateRoleDto);
            if (result.IsSuccessStatusCode == true)
                return true;
            else
                return false;
        }
    }
}
