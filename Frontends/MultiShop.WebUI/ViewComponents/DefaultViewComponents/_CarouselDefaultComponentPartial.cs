using IdentityModel.AspNetCore.AccessTokenManagement;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.WebUI.Services.CatalogServices.SliderServices;
using MultiShop.WebUI.Services.TokenService;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _CarouselDefaultComponentPartial : ViewComponent
    {
        private readonly IFeatureSliderService _featureSliderService; 
        private readonly ITokenService _tokenService;
        private readonly IClientAccessTokenCache _clientAccessTokenCache;

        public _CarouselDefaultComponentPartial(IFeatureSliderService featureSliderService, ITokenService tokenService, IClientAccessTokenCache clientAccessTokenCache)
        {
            _featureSliderService = featureSliderService;
            _tokenService = tokenService;
            _clientAccessTokenCache = clientAccessTokenCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var accessToken = await _clientAccessTokenCache.GetAsync("AccessToken");
            if (accessToken == null)
            {
                var token = _tokenService.CreateToken();
            }
            var values = await _featureSliderService.GetAllFeatureSliderAsync();
            return View(values);
        }
    }
}
