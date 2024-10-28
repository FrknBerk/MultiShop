using Microsoft.AspNetCore.Identity;

namespace MultiShop.IdentityServer.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string Name { get; set; }
    }
}
