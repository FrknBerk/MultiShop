using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.FeatureServices
{
    public class FeatureService : IFeatureService
    {
        private readonly HttpClient _httpClient;

        public FeatureService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateFeatureAsync(CreateFeatureDto FeatureDto)
        {
            await _httpClient.PostAsJsonAsync<CreateFeatureDto>("features", FeatureDto);
        }

        public async Task DeleteFeatureAsync(string id)
        {
            await _httpClient.DeleteAsync("features?id=" + id);
        }

        public async Task<List<ResultFeatureDto>> GetAllFeatureAsync()
        {
            var responseMessage = await _httpClient.GetAsync("features");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);
            return values;
        }

        public async Task<UpdateFeatureDto> GetByIdFeatureAsync(string id)
        {
            var reponseMessage = await _httpClient.GetAsync("features/" + id);
            var values = await reponseMessage.Content.ReadFromJsonAsync<UpdateFeatureDto>();
            return values;
        }

        public async Task UpdateFeatureAsync(UpdateFeatureDto FeatureDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateFeatureDto>("features", FeatureDto);
        }
    }
}
