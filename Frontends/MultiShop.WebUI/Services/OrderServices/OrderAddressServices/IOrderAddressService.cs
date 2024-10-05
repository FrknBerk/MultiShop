using MultiShop.DtoLayer.OrderDtos.OrderAddressDtos;

namespace MultiShop.WebUI.Services.OrderServices.OrderAddressServices
{
    public interface IOrderAddressService
    {
        //Task<List<ResultCategoryDto>> GetAllCategoryAsync();
        Task CreateOrderAddressAsync(CreateOrderAddressDto createOrderAddressDto);
        //Task UpdateCategoryAsync(UpdateCategoryDto categoryDto);
        //Task DeleteCategoryAsync(string id);
        //Task<UpdateCategoryDto> GetByIdCategoryAsync(string id);
    }
}
