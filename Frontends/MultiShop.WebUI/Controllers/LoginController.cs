using IdentityModel.AspNetCore.AccessTokenManagement;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace MultiShop.WebUI.Controllers
{
    public class LoginController : Controller
	{
		private readonly IIdentityService _identityService;
		private readonly IClientAccessTokenCache _clientAccessTokenCache;

        public LoginController(IIdentityService identityService, IClientAccessTokenCache clientAccessTokenCache)
        {
            _identityService = identityService;
            _clientAccessTokenCache = clientAccessTokenCache;
        }

        [HttpGet]
        public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(SignInDto signInDto)
		{
			bool loginIsTrue = await _identityService.SignIn(signInDto);
			if (loginIsTrue)
				return RedirectToAction("Index", "Default");
			else
				return View();
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _clientAccessTokenCache.DeleteAsync("AccessToken");
			return RedirectToAction("Index", "Login");
		}
	}
}
