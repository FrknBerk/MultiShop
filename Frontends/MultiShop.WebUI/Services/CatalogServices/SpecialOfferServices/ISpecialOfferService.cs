using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;

namespace MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices
{
    public interface ISpecialOfferService
    {
        Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync();
        Task CreateSpecialOfferAsync(CreateSpecialOfferDto specialOfferDto);
        Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto specialOfferDto);
        Task DeleteSpecialOfferAsync(string id);
        Task<UpdateSpecialOfferDto> GetByIdSpecialOfferAsync(string id);
    }
}
