using MultiShop.DtoLayer.CatalogDtos.ProductPropertyDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.ProductPropertyServices
{
    public class ProductPropertyService : IProductPropertyService
    {
        private readonly HttpClient _httpClient;

        public ProductPropertyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateProductPropertyAsync(CreateProductPropertyDto createProductPropertyDto)
        {
            await _httpClient.PostAsJsonAsync<CreateProductPropertyDto>("productproperties", createProductPropertyDto);
        }

        public async Task DeleteProductPropertyAsync(string id)
        {
            await _httpClient.DeleteAsync("productproperties?id=" + id);
        }

        public async Task<List<ResultProductPropertyDto>> GetAllProductPropertyAsync()
        {
            var responseMessage = await _httpClient.GetAsync("productproperties");
            var jsonData= await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductPropertyDto>>(jsonData);
            return values;
        }

        public async Task<ResultProductPropertyDto> GetByPropertyIdProductPropertyAsync(string id)
        {
            var reponseMessage = await _httpClient.GetAsync("productproperties/" + id);
            var values = await reponseMessage.Content.ReadFromJsonAsync<ResultProductPropertyDto>();
            return values;
        }

        public async Task UpdateProductPropertyAsync(UpdateProductPropertyDto updateProductPropertyDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateProductPropertyDto>("productproperties", updateProductPropertyDto);
        }
    }
}
