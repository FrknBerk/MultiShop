using MultiShop.DtoLayer.CargoDtos.CargoCustomerDtos;
using System.Net.Http;

namespace MultiShop.WebUI.Services.CargoServices.CargoCustomerServices
{
    public class CargoCustomerService : ICargoCustomerService
    {
        private readonly HttpClient _httpClient;

        public CargoCustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetCargoCustomerByIdDto> GetByIdCargoCustomerAsync(string id)
        {
            var reponseMessage = await _httpClient.GetAsync("CargoCustomers/GetCargoCustomerById?id" + id);
            var values = await reponseMessage.Content.ReadFromJsonAsync<GetCargoCustomerByIdDto>();
            return values;
        }
    }
}
