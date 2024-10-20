using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.IdentityServer.Dtos;
using MultiShop.IdentityServer.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using static IdentityServer4.IdentityServerConstants;
using static IdentityServer4.Models.IdentityResources;

namespace MultiShop.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var userClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            var user = await _userManager.FindByIdAsync(userClaim.Value);
            return Ok(new
            {
                Id = user.Id,
                Name = user.Name,
                SurName = user.Surname,
                Email = user.Email,
                UserName = user.UserName,
            });
        }

        [HttpGet("GetAllUserList")]
        public async Task<IActionResult> GetlAllUserList()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("GetByIdUser")]
        public async Task<IActionResult> GetByIdUser(string id)
        {
            var user = await _userManager.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> UserUpdate(UserUpdateDto userUpdateDto)
        {
            var user = await _userManager.FindByIdAsync(userUpdateDto.Id);
            if (user == null)
                return Ok("Kullanıcı bulunamadı");
            user.UserName = userUpdateDto.UserName;
            user.PhoneNumber = userUpdateDto.PhoneNumber;
            user.Email = userUpdateDto.Email;
            user.Surname = userUpdateDto.Surname;
            user.AddressDescription = userUpdateDto.AddressDescription;
            user.AddressTitle = userUpdateDto.AddressTitle;
            user.Birthday = userUpdateDto.Birthday;
            user.City = userUpdateDto.City;
            user.District = userUpdateDto.District;
            user.Name = userUpdateDto.Name;
            user.Neighbourhood = userUpdateDto.Neighbourhood;
            var result = await _userManager.UpdateAsync(user);
            if(result.Succeeded)
            {
                return Ok("başarıyla güncellendi");
            }
            else
            {
                return Ok("Kullanıcı güncellenemedi");
            }
        }
    }
}
