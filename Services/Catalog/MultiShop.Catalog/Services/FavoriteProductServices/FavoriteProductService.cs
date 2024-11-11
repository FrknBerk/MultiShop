using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Dtos.FavoriteProductDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.FavoriteProductServices
{
    public class FavoriteProductService : IFavoriteProductService
    {
        private readonly IMongoCollection<FavoriteProduct> _favoriteProductCollection;
        private readonly IMapper _mapper;

        public FavoriteProductService(IDatabaseSettings _databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(_databaseSettings.ConnectString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _favoriteProductCollection = database.GetCollection<FavoriteProduct>(_databaseSettings.FavoriteProductCollectionName);
            _mapper = mapper;
        }
        public async Task CreateFavoriteProductAsync(CreateFavoriteProductDto createFavoriteProductDto)
        {
            var result = await _favoriteProductCollection.Find<FavoriteProduct>(x => x.ProductId == createFavoriteProductDto.ProductId && x.UserId == createFavoriteProductDto.UserId).FirstOrDefaultAsync();
            if (result == null)
            {
                var value = _mapper.Map<FavoriteProduct>(createFavoriteProductDto);
                await _favoriteProductCollection.InsertOneAsync(value);
            }
        }

        public async Task DeleteFavoriteProductAsync(string id)
        {
            await _favoriteProductCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<ResultFavoriteProductDto>> GetByUserIdListAsync(string userId)
        {
            var values = await _favoriteProductCollection.Find<FavoriteProduct>(x => x.UserId == userId).ToListAsync();
            return _mapper.Map<List<ResultFavoriteProductDto>>(values);
        }
    }
}
