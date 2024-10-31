using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.IdentityServices.UserIdentityServices;
using MultiShop.WebUI.Services.Interface;

namespace MultiShop.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserIdentityService _userIdentityService;

        public UserController(IUserService userService, IUserIdentityService userIdentityService)
        {
            _userService = userService;
            _userIdentityService = userIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _userService.GetUserInfo();
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> GetByIdUser(string id)
        {
            var values = await _userIdentityService.GetByIdUser(id);
            return Ok(values);
        }
    }
}
