
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.Interface;
using System.Net;
using System.Net.Http.Headers;

namespace MultiShop.WebUI.Handlers
{
    public class ClientCredentialTokenHandler : DelegatingHandler
    {
        private readonly IClientCredentialTokenService _credentialTokenService;

        public ClientCredentialTokenHandler(IClientCredentialTokenService credentialTokenService)
        {
            _credentialTokenService = credentialTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _credentialTokenService.GetToken());
            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                //hata mesajı
            }
            return response;
        }
    }
}
