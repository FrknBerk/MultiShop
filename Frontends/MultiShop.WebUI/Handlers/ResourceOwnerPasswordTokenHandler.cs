
using IdentityModel.AspNetCore.AccessTokenManagement;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MultiShop.WebUI.Services.Interface;
using System.Net;
using System.Net.Http.Headers;

namespace MultiShop.WebUI.Handlers
{
    public class ResourceOwnerPasswordTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityService _identityService;
        private readonly IClientAccessTokenCache _clientAccessTokenCache;

        public ResourceOwnerPasswordTokenHandler(IHttpContextAccessor httpContextAccessor, IIdentityService identityService, IClientAccessTokenCache clientAccessTokenCache)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityService = identityService;
            _clientAccessTokenCache = clientAccessTokenCache;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            var accessToken = await _clientAccessTokenCache.GetAsync("AccessToken");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.AccessToken);
            var response = await base.SendAsync(request, cancellationToken);
            
            if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                //var tokenResponse = await _identityService.GetRefreshToken();

                //if(tokenResponse != null)
                //{
                //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse);
                //    response = await base.SendAsync(request, cancellationToken);
                //}
            }

            if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                //hata mesajı
            }
            return response;
        }
    }
}
