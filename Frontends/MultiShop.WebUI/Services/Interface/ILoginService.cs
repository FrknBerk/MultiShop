using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.DtoLayer.IdentityDtos.UserDtos;

namespace MultiShop.WebUI.Services.Interface
{
    public interface ILoginService
    {
        public string GetUserId { get; }
        Task<bool> RefreshPassword(RefreshPasswordDto refreshPasswordDto);

        Task<ResultUserDto> GetByEmailUserAsync(string email);
    }
}
