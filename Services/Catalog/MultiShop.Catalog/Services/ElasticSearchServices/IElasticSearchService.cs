using Elastic.Clients.Elasticsearch;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Entities.Abstract;
using Nest;
using System.Linq.Expressions;

namespace MultiShop.Catalog.Services.ElasticSearchServices
{
    public interface IElasticSearchService
    {
        Task<bool> CreateIndexAsync(string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default);

        Task<bool> CreateDocumentAsync<T>(T document, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default) where T : IElasticsearchModal;

        Task<bool> UpdateDocumentAsync<T>(string documentId, object partialDocument, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default);

        Task<bool> DeleteDocumentAsync<T>(string documentId, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default);

        Task<long> CountDocumentsAsync<T>(string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default) where T : IElasticsearchModal;

        Task<IReadOnlyCollection<T>> GetDocumentsAsync<T>(string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default) where T : IElasticsearchModal;

        Task<T> GetDocumentAsync<T>(string documentId, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default) where T : IElasticsearchModal;

        Task<IReadOnlyCollection<T>> SearchAsync<T>(Action<SearchRequestDescriptor<T>> searchRequestDescriptor, CancellationToken cancellationToken = default) where T : IElasticsearchModal;

        Task<IReadOnlyCollection<T>> MatchQueryAsync<T>(Expression<Func<T, object>> field, string queryKeyword, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default, int from = 0, int size = 10) where T : IElasticsearchModal;

        Task<IReadOnlyCollection<T>> FuzzyQueryAsync<T>(Expression<Func<T, object>> field, string queryKeyword, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default, int from = 0, int size = 10) where T : IElasticsearchModal;

        Task<IReadOnlyCollection<T>> WildcardQueryAsync<T>(Expression<Func<T, object>> field, string queryKeyword, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default, int from = 0, int size = 10) where T : IElasticsearchModal;

        Task<IReadOnlyCollection<T>> BoolQueryAsync<T>(Expression<Func<T, object>> matchField, string matchQueryKeyword, Expression<Func<T, object>> fuzzyField, string fuzzyQueryKeyword, Expression<Func<T, object>> wildcardField, string wildcardQueryKeyword, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default, int from = 0, int size = 10) where T : IElasticsearchModal;

        Task<IReadOnlyCollection<T>> TermQueryAsync<T>(Expression<Func<T, object>> field, string queryKeyword, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default, int from = 0, int size = 10) where T : IElasticsearchModal;

        Task<IReadOnlyCollection<T>> ExistsQueryAsync<T>(Expression<Func<T, object>> field, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default, int from = 0, int size = 10) where T : IElasticsearchModal;
    }
}
