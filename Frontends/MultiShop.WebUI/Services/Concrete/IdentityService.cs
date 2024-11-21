using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.WebUI.Services.IdentityServices.RoleIdentityServices;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.TokenService;
using MultiShop.WebUI.Settings;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Claims;

namespace MultiShop.WebUI.Services.Concrete
{
    public class IdentityService : IIdentityService
    {
        private readonly ITokenService _tokenService;

        public IdentityService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<bool> SignIn(SignInDto signInDto)
        {
            var role = await GetUserNameRole(signInDto.UserName);
            await _tokenService.ClearToken();
            var token = await _tokenService.CreateLoginToken(signInDto);
            if (token != null)
                return true;
            return false;
        }
        public async Task<string> GetUserNameRole(string userName)
        {
            var option = new RestClientOptions("http://localhost:5001");
            var client = new RestClient(option);
            var request = new RestRequest("api/Roles/GetUserNameRole?userName=" + userName);
            RestResponse responseMessage = await client.ExecuteAsync(request);
            var jsonData = responseMessage.Content;
            var values = JsonConvert.DeserializeObject<string>(jsonData);
            return values;
        }
    }
}
