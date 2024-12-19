using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductPropertyDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductPropertyServices
{
    public class ProductPropertyService : IProductPropertyService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<ProductProperty> _productPropertyCollection;

        public ProductPropertyService(IMapper mapper,IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productPropertyCollection = database.GetCollection<ProductProperty>(_databaseSettings.ProductPropertyCollectionName);
            _mapper = mapper;
        }

        public async Task CreateProductPropertyAsync(CreateProductPropertyDto createProductPropertyDto)
        {
            var values = _mapper.Map<ProductProperty>(createProductPropertyDto);
            await _productPropertyCollection.InsertOneAsync(values);
        }

        public async Task DeleteProductPropertyAsync(string id)
        {
            await _productPropertyCollection.DeleteOneAsync(x => x.ProductId == id);
        }

        public async Task<List<ResultProductPropertyDto>> GetAllProductPropertyAsync()
        {
            var values = await _productPropertyCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultProductPropertyDto>>(values);
        }

        public async Task<ResultProductPropertyDto> GetByIdProductPropertyAsync(string id)
        {
            var values = await _productPropertyCollection.Find(x => x.PropertyId == id).FirstOrDefaultAsync();
            return _mapper.Map<ResultProductPropertyDto>(values);
        }

        public async Task UpdateProductPropertyAsync(UpdateProductPropertyDto updateProductPropertyDto)
        {
            var values = _mapper.Map<ProductProperty>(updateProductPropertyDto);
            await _productPropertyCollection.FindOneAndReplaceAsync(x => x.PropertyId == updateProductPropertyDto.PropertyId, values);
        }
    }
}
