using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly HttpClient _httpClient;

        public ProductImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateProductImageAsync(CreateProductImageDto ProductImageDto)
        {
            await _httpClient.PostAsJsonAsync<CreateProductImageDto>("productimages", ProductImageDto);
        }

        public async Task DeleteProductImageAsync(string id)
        {
            await _httpClient.DeleteAsync("productimages?id=" + id);
        }

        public async Task<List<ResultProductImageDto>> GetAllProductImageAsync()
        {
            var responseMessage = await _httpClient.GetAsync("productimages");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductImageDto>>(jsonData);
            return values;
        }

        public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
        {
            var reponseMessage = await _httpClient.GetAsync("productimages/" + id);
            var values = await reponseMessage.Content.ReadFromJsonAsync<GetByIdProductImageDto>();
            return values;
        }

        public async Task<List<GetByIdProductImageDto>> GetByProductIdProductImageAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("productimages/ProductImagesByProductId?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<GetByIdProductImageDto>>(jsonData);
            return values;
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDto ProductImageDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateProductImageDto>("productimages", ProductImageDto);
        }

    }
}
