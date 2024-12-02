using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.IdentityServer.Dtos.LoginDtos;
using MultiShop.IdentityServer.Models;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace MultiShop.IdentityServer.Controllers
{
    [AllowAnonymous]
    //[Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegistersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> UserRegister(UserRegisterDto registerDto)
        {
            var values = new ApplicationUser()
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                Birthday = registerDto.Birthday,
                AddressDescription = registerDto.AddressDescription,
                AddressTitle = registerDto.AddressTitle,
                City = registerDto.City,
                District = registerDto.District,
                Neighbourhood = registerDto.Neighbourhood,
                PhoneNumber = registerDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(values, registerDto.Password);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("IfExistsUserName")]
        public async Task<IActionResult> IfExistsUserNameAsync(string username)
        {
            var result = await _userManager.Users.AnyAsync<ApplicationUser>(x => x.UserName == username);
            if (result)
                return Ok($"{username} kullanıcı adı var");
            else
                return BadRequest("Yeni Kullanıcı ekle");
            
        }

        [HttpGet("IfExistsEmail")]
        public async Task<IActionResult> IfExistsEmailAsync(string email)
        {
            var result = await _userManager.Users.AnyAsync<ApplicationUser>(x => x.Email == email);
            if (result)
                return Ok($"{email} kayıtlı");
            else
                return BadRequest("Yeni kullanıcı ekle");

        }
    }
}
