using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.UserDtos;
using MultiShop.WebUI.Services.IdentityServices.UserIdentityServices;
using MultiShop.WebUI.Services.Interface;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("User")]
    public class ProfileController : Controller
    {
        private readonly IUserIdentityService _userIdentityService;
        private readonly IUserService _userService;

        public ProfileController(IUserIdentityService userIdentityService, IUserService userService)
        {
            _userIdentityService = userIdentityService;
            _userService = userService;
        }
        public async Task<IActionResult> MyProfile()
        {
            var user = await _userService.GetUserInfo();
            var values = await _userIdentityService.GetByIdUser(user.Id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> MyProfile(UpdateUserDto resultUserDto)
        {
            var user = await _userService.GetUserInfo();
            resultUserDto.Id = user.Id;
            await _userIdentityService.UpdateUser(resultUserDto);
            var values = await _userIdentityService.GetByIdUser(resultUserDto.Id);
            return View(values);
        }
    }
}
