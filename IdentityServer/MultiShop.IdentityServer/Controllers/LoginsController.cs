using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.Dtos;
using MultiShop.IdentityServer.Models;
using MultiShop.IdentityServer.Tools;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

		public LoginsController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IHttpClientFactory httpClientFactory)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_httpClientFactory = httpClientFactory;
		}

		[HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginDto userLoginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(userLoginDto.UserName,userLoginDto.Password,false,false);
            var user = await _userManager.FindByNameAsync(userLoginDto.UserName);
			if (result.Succeeded)
            {
				//var client = _httpClientFactory.CreateClient();
				//var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5001/connect/token");
				//var collection = new List<KeyValuePair<string, string>>();
				//collection.Add(new KeyValuePair<string, string>("client_id", "MultiShopVisitorId"));
				//collection.Add(new KeyValuePair<string, string>("client_secret", "multishopsecret"));
				//collection.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
				//var content = new FormUrlEncodedContent(collection);
				//request.Content = content;
				//var responseMessage = await client.SendAsync(request);
				//responseMessage.EnsureSuccessStatusCode();
				//return Ok("Giriş Başarılı");
				GetCheckAppUserViewModel model = new GetCheckAppUserViewModel();
				model.UserName = userLoginDto.UserName;
				model.Id = user.Id;
				var token = JwtTokenGenerator.GenerateToken(model);
				return Ok(token);
				//return Ok(responseMessage.Content.ReadAsStringAsync());
			}
            else
            {
                return BadRequest("Kullanıcı adı ve şifre yanlış");
            }
        }
    }
}
