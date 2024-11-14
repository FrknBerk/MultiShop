namespace MultiShop.IdentityServer.Dtos.LoginDtos
{
    public class RefreshPasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
