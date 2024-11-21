using Nest;

namespace MultiShop.Catalog.Services.ElasticSearchServices
{
    public interface IElasticSearchConnect
    {
        ElasticClient EsClient();
    }
}
