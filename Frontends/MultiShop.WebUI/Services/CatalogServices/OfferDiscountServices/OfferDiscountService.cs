using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices
{
    public class OfferDiscountService : IOfferDiscountService
    {
        private readonly HttpClient _httpClient;

        public OfferDiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateOfferDiscountAsync(CreateOfferDiscountDto OfferDiscountDto)
        {
            await _httpClient.PostAsJsonAsync<CreateOfferDiscountDto>("offerdiscounts", OfferDiscountDto);
        }

        public async Task DeleteOfferDiscountAsync(string id)
        {
            await _httpClient.DeleteAsync("offerdiscounts?id=" + id);
        }

        public async Task<List<ResultOfferDiscountDto>> GetAllOfferDiscountAsync()
        {
            var responseMessage = await _httpClient.GetAsync("offerdiscounts");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultOfferDiscountDto>>(jsonData);
            return values;
        }

        public async Task<UpdateOfferDiscountDto> GetByIdOfferDiscountAsync(string id)
        {
            var reponseMessage = await _httpClient.GetAsync("offerdiscounts/" + id);
            var values = await reponseMessage.Content.ReadFromJsonAsync<UpdateOfferDiscountDto>();
            return values;
        }

        public async Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto OfferDiscountDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateOfferDiscountDto>("offerdiscounts", OfferDiscountDto);
        }
    }
}
