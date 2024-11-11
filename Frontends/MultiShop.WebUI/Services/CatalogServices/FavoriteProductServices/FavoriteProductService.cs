using MultiShop.DtoLayer.CatalogDtos.FavoriteProductDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.FavoriteProductServices
{
    public class FavoriteProductService : IFavoriteProductService
    {
        private readonly HttpClient _httpClient;

        public FavoriteProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CreateFavoriteProductAsync(CreateFavoriteProductDto createFavoriteProductDto)
        {
            var result =await _httpClient.PostAsJsonAsync<CreateFavoriteProductDto>("favoriteproducts", createFavoriteProductDto);
        }

        public async Task DeleteFavoriteProductAsync(string id)
        {
            await _httpClient.DeleteAsync("favoriteproducts?id="+id);
            
        }

        public async Task<List<ResultFavoriteProductDto>> GetByUserIdFavoriteProdutAsync(string userId)
        {
            var responseMessage = await _httpClient.GetAsync("favoriteproducts/" + userId);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFavoriteProductDto>>(jsonData);
            return values;
        }
    }
}
