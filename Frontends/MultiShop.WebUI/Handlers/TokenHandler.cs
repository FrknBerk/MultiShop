
using System.Net.Http.Headers;
using System.Net;
using IdentityModel.AspNetCore.AccessTokenManagement;
using MultiShop.WebUI.Services.TokenService;

namespace MultiShop.WebUI.Handlers
{
    public class TokenHandler : DelegatingHandler
    {
        private readonly IClientAccessTokenCache _clientAccessTokenCache;
        private readonly ITokenService _tokenService;

        public TokenHandler(IClientAccessTokenCache clientAccessTokenCache, ITokenService tokenService)
        {
            _clientAccessTokenCache = clientAccessTokenCache;
            _tokenService = tokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _clientAccessTokenCache.GetAsync("AccessToken");
            if (accessToken == null)
            {
                var token = _tokenService.CreateToken();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
            }
            else
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.AccessToken);
            }
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                //hata mesajı
            }
            return response;
        }
    }
}
