using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MultiShop.IdentityServer.Models
{
    public class UserAddress : IdentityUser
    {
        [Key]
        public string AddressId { get; set; }
        public string AddressTitle { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Neighbourhood { get; set; }
        public string AddressDescription { get; set; }
        //public IdentityUser User { get; set; }
    }
}
