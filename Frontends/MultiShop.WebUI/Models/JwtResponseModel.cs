namespace MultiShop.WebUI.Models
{
    public class JwtResponseModel
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
        public string TokenType { get; set; }
    }
}
