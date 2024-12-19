using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductPropertyDtos;
using MultiShop.Catalog.Dtos.PropertyTypeDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.PropertyTypeServices
{
    public class PropertyTypeService : IPropertyTypeService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<PropertyType> _propertyTypeCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        public PropertyTypeService(IMapper mapper,IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _propertyTypeCollection = database.GetCollection<PropertyType>(_databaseSettings.PropertyTypeCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }


        public async Task CreatePropertyTypeAsync(CreatePropertyTypeDto createPropertyTypeDto)
        {
            var values = _mapper.Map<PropertyType>(createPropertyTypeDto);
            await _propertyTypeCollection.InsertOneAsync(values);
        }

        public async Task DeletePropertyTypeAsync(string id)
        {
            await _propertyTypeCollection.DeleteOneAsync(x => x.PropertyId == id);
        }

        public async Task<List<ResultPropertyTypeDto>> GetAllPropertyTypeAsync()
        {
            var values = await _propertyTypeCollection.Find(x => true).ToListAsync();
            foreach (var item in values)
            {
                item.Category = await _categoryCollection.Find<Category>(x => x.CategoryId == item.CategoryId).FirstAsync();
            }
            return _mapper.Map<List<ResultPropertyTypeDto>>(values);
        }

        public async Task<UpdatePropertyTypeDto> GetByIdPropertyTypeAsync(string id)
        {
            var values = await _propertyTypeCollection.Find(x => x.PropertyId == id).FirstOrDefaultAsync();
            return _mapper.Map<UpdatePropertyTypeDto>(values);
        }

        public async Task UpdatePropertyTypeAsync(UpdatePropertyTypeDto updatePropertyTypeDto)
        {
            var values = _mapper.Map<PropertyType>(updatePropertyTypeDto);
            await _propertyTypeCollection.FindOneAndReplaceAsync(x => x.PropertyId == updatePropertyTypeDto.PropertyId, values);
        }
    }
}
