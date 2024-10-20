using MultiShop.DtoLayer.IdentityDtos.RegisterDtos;
using MultiShop.DtoLayer.IdentityDtos.UserDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Services.IdentityServices.UserIdentityServices
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly HttpClient _httpClient;

        public UserIdentityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ResultUserDto>> GetAllUserListAsync()
        {
            var responseMessage = await _httpClient.GetAsync("api/users/GetAllUserList");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultUserDto>>(jsonData);
            return values;
        }

        public async Task<UpdateUserDto> GetByIdUser(string id)
        {
            var responseMessage = await _httpClient.GetAsync("api/users/GetByIdUser?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateUserDto>(jsonData);
            return values;
        }

        public async Task UpdateUser(UpdateUserDto user)
        {
            await _httpClient.PutAsJsonAsync<UpdateUserDto>("api/users", user);
        }
    }
}
