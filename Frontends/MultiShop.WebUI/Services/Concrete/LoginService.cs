using System.Net.Http.Json;
using System.Security.Claims;
using Elastic.Clients.Elasticsearch;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MongoDB.Driver.Core.WireProtocol.Messages;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.DtoLayer.IdentityDtos.RegisterDtos;
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

        public async Task<bool> IfExistsEmailAsync(string email)
        {
            var responseMessage = await _httpClient.GetAsync("http://localhost:5001/api/Registers/IfExistsEmail?username=" + email);
            if (responseMessage.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> IfExistsUserNameAsync(string userName)
        {
            var responseMessage = await _httpClient.GetAsync("http://localhost:5001/api/Registers/IfExistsUserName?username=" + userName);
            if (responseMessage.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> RefreshPassword(RefreshPasswordDto refreshPasswordDto)
        {
            var result = await _httpClient.PostAsJsonAsync<RefreshPasswordDto>("http://localhost:5001/api/logins/RefreshPassword", refreshPasswordDto);
            if (result.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> RegisterUserAsync(CreateRegisterDto createRegisterDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CreateRegisterDto>("http://localhost:5001/api/Registers/", createRegisterDto);
            if (result.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
