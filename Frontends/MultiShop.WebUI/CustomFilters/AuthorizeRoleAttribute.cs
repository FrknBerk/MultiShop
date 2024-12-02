using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.Interface;

namespace MultiShop.WebUI.CustomFilters
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        private readonly IUserService _userService;
        private readonly IIdentityService _identityService;
        public string[] RoleName { get; set; }

        public AuthorizeRoleAttribute(string[] roleName)
        {
            RoleName = roleName;
        }

        

        public string Roles()
        {
            var user = _userService.GetUserInfo();
            var role = _identityService.GetUserNameRole(user.Result.UserName).ToString();
            if (role == "Admin")
                return "Admin";
            if (role == "Manager")
                return "Manager";
            return "Visitor";
        }

    }
}
