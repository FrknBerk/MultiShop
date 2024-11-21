using IdentityModel.Client;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;

namespace MultiShop.WebUI.Services.TokenService
{
    public interface ITokenService
    {
        Task<string> GetUserNameRole(string userName);
        Task<string> CreateLoginToken(SignInDto signInDto);
        Task<string> CreateToken();
        Task ClearToken();
    }
}
