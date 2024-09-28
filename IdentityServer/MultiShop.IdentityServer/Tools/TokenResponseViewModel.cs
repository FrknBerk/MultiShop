using System;

namespace MultiShop.IdentityServer.Tools
{
	public class TokenResponseViewModel
	{
        public string Token { get; set; }

		public TokenResponseViewModel(string token, DateTime expireDate)
		{
			Token = token;
			ExpireDate = expireDate;
		}

		public DateTime ExpireDate { get; set; }
    }
}
