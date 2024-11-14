using System.Security.Claims;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.DtoLayer.IdentityDtos.UserDtos;
using MultiShop.WebUI.Services.Interface;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.Concrete
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginService(IHttpContextAccessor contextAccessor, HttpClient httpClient)
        {
            _contextAccessor = contextAccessor;
            _httpClient = httpClient;
        }

        public string GetUserId => _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        public async Task<ResultUserDto> GetByEmailUserAsync(string email)
        {
            var responseMessage = await _httpClient.GetAsync("http://localhost:5001/api/logins/GetByEmailUser?email="+email);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<ResultUserDto>(jsonData);
            return values;
        }

        public async Task<bool> RefreshPassword(RefreshPasswordDto refreshPasswordDto)
        {
            var result = await _httpClient.PostAsJsonAsync<RefreshPasswordDto>("http://localhost:5001/api/logins/RefreshPassword", refreshPasswordDto);
            if (result.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
