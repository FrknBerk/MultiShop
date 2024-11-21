
using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.WebUI.Settings;
using Newtonsoft.Json;
using RestSharp;
using System.Data;
using System.Net.Http;

namespace MultiShop.WebUI.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly IClientAccessTokenCache _clientAccessTokenCache;
        private readonly HttpClient _httpClient;
        private readonly ServiceApiSettings _serviceApiSettings;
        private readonly ClientSettings _clientSettings;

        public TokenService(IClientAccessTokenCache clientAccessTokenCache, HttpClient httpClient, IOptions<ServiceApiSettings> serviceApiSettings, IOptions<ClientSettings> clientSettings)
        {
            _clientAccessTokenCache = clientAccessTokenCache;
            _httpClient = httpClient;
            _serviceApiSettings = serviceApiSettings.Value;
            _clientSettings = clientSettings.Value;
        }

        public async Task ClearToken()
        {
            await _clientAccessTokenCache.DeleteAsync("AccessToken");
        }

        public async Task<string> CreateLoginToken(SignInDto signInDto)
        {
            var roleName = await GetUserNameRole(signInDto.UserName);
            var currentToken = await _clientAccessTokenCache.GetAsync("AccessToken");
            if (currentToken != null)
            {
                return currentToken.ToString();
            }
            var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityServerUrl,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false
                }
            });
            var clientId = "";
            var clientSecret = "";
            if (roleName == "Admin")
            {
                clientId = _clientSettings.MultiShopAdminClient.ClientId;
                clientSecret = _clientSettings.MultiShopAdminClient.ClientSecret;
            }
            if (roleName == "Manager")
            {
                clientId = _clientSettings.MultiShopManagerClient.ClientId;
                clientSecret = _clientSettings.MultiShopManagerClient.ClientSecret;
            }
            if (roleName == "Visitor" || roleName ==null)
            {
                clientId = _clientSettings.MultiShopVisitorClient.ClientId;
                clientSecret = _clientSettings.MultiShopVisitorClient.ClientSecret;
            }

            var postmanTokenRequest = new PasswordTokenRequest
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                UserName = signInDto.UserName,
                Password = signInDto.Password,
                Address = discoveryEndPoint.TokenEndpoint
            };
            var token = await _httpClient.RequestPasswordTokenAsync(postmanTokenRequest);

            if (token.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                await _clientAccessTokenCache.SetAsync("AccessToken", token.AccessToken, token.ExpiresIn);
                return token.ToString();
            }
            else
            {
                return null;
            }
        }

        public async Task<string> CreateToken()
        {
            var currentToken = await _clientAccessTokenCache.GetAsync("AccessToken");
            if (currentToken != null)
            {
                return null;
            }
            var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityServerUrl,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false
                }
            });
            var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = _clientSettings.MultiShopVisitorClient.ClientId,
                ClientSecret = _clientSettings.MultiShopVisitorClient.ClientSecret,
                Address = discoveryEndPoint.TokenEndpoint
            };

            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);
            await _clientAccessTokenCache.SetAsync("AccessToken", newToken.AccessToken, newToken.ExpiresIn);
            return newToken.AccessToken;
        }

        public async Task<string> GetUserNameRole(string userName)
        {
            var option = new RestClientOptions("http://localhost:5001");
            var client = new RestClient(option);
            var request = new RestRequest("api/Roles/GetUserNameRole?userName=" + userName);
            RestResponse responseMessage = await client.ExecuteAsync(request);
            var jsonData = responseMessage.Content;
            var values = JsonConvert.DeserializeObject<string>(jsonData);
            return values;
        }

    }
}
