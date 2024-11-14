using IdentityModel.AspNetCore.AccessTokenManagement;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.DtoLayer.MailDtos;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.MailServices;
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
		private readonly IMailService _mailService;
		private readonly ILoginService _loginService;

        public LoginController(IIdentityService identityService, IClientAccessTokenCache clientAccessTokenCache, IMailService mailService, ILoginService loginService)
        {
            _identityService = identityService;
            _clientAccessTokenCache = clientAccessTokenCache;
            _mailService = mailService;
            _loginService = loginService;
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

		[HttpGet]
		public IActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgotPassword(string email)
		{
			var user = await _loginService.GetByEmailUserAsync(email);
			var createMail = new CreateMailSendDto
			{
				Subject = "Şifre Yenileme Talebi",
				Body = "<div style=\"padding:30px;margin:10px;\">\r\n       <h1>Sayın "+ user.Name + " " + user.Surname + "<span>,</span></h1\r\n       <br/>\r\n       <p>Hesabınıza giriş için kullandığınız güvenlik şifresi değiştirilmiştir. Yeni şifreniz ile giriş yapabilirsiniz. </p><br/>\r\n       <a href='https://localhost:7080/Login/RefreshPasswordPage?email="+ email + "' style='background-color:orange;color:white;border: 2px solid orange;padding: 10px;border-radius: 10px;'>Yeni Sifre ile Giris Yap</a>\r\n       <br/><br/>\r\n       <p>Teşekkür Ederiz,</p>\r\n       <h4><span>Multi Shop</span></h4>\r\n     </div>",
				To = email,
				From = "personelberk@gmail.com"
            };
			_mailService.SendMail(createMail);
			return View();
		}

		public IActionResult RefreshPasswordPage(string email)
		{
			ViewBag.Email = email;
			return View();
		}

		public async Task<IActionResult> RefreshPassword(string password,string email)
		{
			var refreshPasswordDto = new RefreshPasswordDto
			{
				Email = email,
				NewPassword = password,
			};
			var result = await _loginService.RefreshPassword(refreshPasswordDto);
			if (result)
				return RedirectToAction("Index","Login",null);
			else
				return View();
		}
    }
}
