using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.RegisterDtos;
using MultiShop.DtoLayer.IdentityDtos.UserRoleDtos;
using MultiShop.WebUI.Services.IdentityServices.RoleIdentityServices;
using MultiShop.WebUI.Services.Interface;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Controllers
{
    public class RegisterController : Controller
    {
		private readonly ILoginService _loginService;
        private readonly IRoleIdentityService _roleIdentityService;

		public RegisterController(ILoginService loginService, IRoleIdentityService roleIdentityService)
		{
			_loginService = loginService;
			_roleIdentityService = roleIdentityService;
		}
		[HttpGet]
        public IActionResult Index()
        {
            return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(CreateRegisterDto createRegisterDto)
		{
			if(createRegisterDto.Password == createRegisterDto.ConfirmPassword)
			{
				var ifExistsUserName = await _loginService.IfExistsUserNameAsync(createRegisterDto.Username);
				if (ifExistsUserName)
				{
                    TempData["ToastrMessage"] = $"{createRegisterDto.Username} kullanıcı adı kullanılıyor";
                    TempData["ToastrType"] = "error";
					return View();
                }
                var ifExistsEmail = await _loginService.IfExistsEmailAsync(createRegisterDto.Email);
                if(ifExistsEmail)
                {
                    TempData["ToastrMessage"] = $"{createRegisterDto.Email} kullanılıyor";
                    TempData["ToastrType"] = "error";
                    return View();
                }
				var register = await _loginService.RegisterUserAsync(createRegisterDto);
				if(register)
				{
					var user = await _loginService.GetByEmailUserAsync(createRegisterDto.Email);
					var role = await _roleIdentityService.GetRoleNameAsync("Visitor");
					var addRole = new CreateUserRoleDto
					{
						RoleId = role.Id,
						UserId = user.Id
					};
					var result = await _roleIdentityService.CreateUserRoleAsync(addRole);
					TempData["ToastrMessage"] = "Giriş Başarılı";
                    TempData["ToastrType"] = "success";
                    return RedirectToAction("Index", "Login");
                }
                return View();
			}
			else
            {
                TempData["ToastrMessage"] = "Şifreler Aynı olmalı";
                TempData["ToastrType"] = "error";
                return View();
            }
		}
	}
}
