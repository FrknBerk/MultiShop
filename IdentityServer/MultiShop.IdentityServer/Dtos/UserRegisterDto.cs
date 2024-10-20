using System;

namespace MultiShop.IdentityServer.Dtos
{
    public class UserRegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public string AddressTitle { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Neighbourhood { get; set; }
        public string AddressDescription { get; set; }
        public string PhoneNumber { get; set; }
    }
}
