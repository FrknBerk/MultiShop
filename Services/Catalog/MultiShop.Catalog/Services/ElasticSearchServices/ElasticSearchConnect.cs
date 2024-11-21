using Elasticsearch.Net;
using Nest;

namespace MultiShop.Catalog.Services.ElasticSearchServices
{
    public class ElasticSearchConnect : IElasticSearchConnect
    {
        public ElasticClient EsClient()
        {
            var nodes = new Uri[]
            {
                new Uri("http://localhost:9200/"),
            };
            var connectionPool = new StaticConnectionPool(nodes);
            var connectionSettings = new ConnectionSettings(connectionPool).DisableDirectStreaming();
            var elasticClient = new ElasticClient(connectionSettings.DefaultIndex("products"));
            return elasticClient;
        }
    }
}
