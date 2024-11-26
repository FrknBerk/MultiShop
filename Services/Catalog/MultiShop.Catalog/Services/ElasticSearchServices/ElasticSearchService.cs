using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.Extensions.Options;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Entities.Abstract;
using System.Linq.Expressions;

namespace MultiShop.Catalog.Services.ElasticSearchServices
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly ElasticsearchClient _elasticsearchClient;

        public ElasticSearchService(IOptions<ElasticsearchClient> elasticsearchClient)
        {
            _elasticsearchClient = elasticsearchClient.Value;
        }

        /// <summary>
        /// Elastis Search index ismini oluşturuyoruz Product gibi
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> CreateIndexAsync(string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default)
        {
            Elastic.Clients.Elasticsearch.IndexResponse indexResponse = await _elasticsearchClient.IndexAsync(indexName, cancellationToken);

            return indexResponse.IsSuccess();
        }

        /// <summary>
        /// Ürün ekleme işlemi gerçekleştiriyoruz
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="document"></param>
        /// <param name="indexName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> CreateDocumentAsync<T>(T document, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default) where T : IElasticsearchModal
        {
            Elastic.Clients.Elasticsearch.CreateRequest<T> createRequest = new(document, indexName, document.Id);
            Elastic.Clients.Elasticsearch.CreateResponse createResponse = await _elasticsearchClient.CreateAsync(createRequest, cancellationToken);

            return createResponse.IsSuccess();
        }

        /// <summary>
        /// Güncelleme işlemi gerçekleştiriyoruz
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="documentId"></param>
        /// <param name="partialDocument"></param>
        /// <param name="indexName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> UpdateDocumentAsync<T>(string documentId, object partialDocument, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default)
        {
            Elastic.Clients.Elasticsearch.UpdateRequest<T, object> updateRequest = new(indexName, documentId)
            {
                Doc = partialDocument
            };
            Elastic.Clients.Elasticsearch.UpdateResponse<T> updateResponse = await _elasticsearchClient.UpdateAsync<T, object>(updateRequest, cancellationToken);

            return updateResponse.IsSuccess();
        }

        /// <summary>
        /// Silme işlemi gerçekleştiriyoruz
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="documentId"></param>
        /// <param name="indexName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> DeleteDocumentAsync<T>(string documentId, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default)
        {
            Elastic.Clients.Elasticsearch.DeleteResponse deleteResponse = await _elasticsearchClient.DeleteAsync<T>(indexName, documentId, cancellationToken);

            return deleteResponse.IsSuccess();
        }

        /// <summary>
        /// index name ile kaç adet olduğunu görüyoruz
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="indexName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<long> CountDocumentsAsync<T>(string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default) where T : IElasticsearchModal
        {
            Elastic.Clients.Elasticsearch.CountResponse countResponse = await _elasticsearchClient.CountAsync<T>(indexName, cancellationToken);

            return countResponse.Count;
        }

        /// <summary>
        /// Elasticsearchte asenkron arama yapmak genellikle büyük veri kümelerinde sorguları çalıştırır
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searchRequestDescriptor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<T>> SearchAsync<T>(Action<SearchRequestDescriptor<T>> searchRequestDescriptor, CancellationToken cancellationToken = default) where T : IElasticsearchModal
        {
            Elastic.Clients.Elasticsearch.SearchResponse<T> searchResponse = await _elasticsearchClient.SearchAsync<T>(searchRequestDescriptor, cancellationToken);

            return searchResponse.Documents;
        }

        /// <summary>
        /// indexname ile bütün o index'e ait verileri getirir.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="indexName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<T>> GetDocumentsAsync<T>(string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default) where T : IElasticsearchModal
        {
            Elastic.Clients.Elasticsearch.SearchResponse<T> searchResponse = await _elasticsearchClient.SearchAsync<T>(indexName, cancellationToken);

            return searchResponse.Documents;
        }

        /// <summary>
        /// Id ye göre veri getiriyor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="documentId"></param>
        /// <param name="indexName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<T> GetDocumentAsync<T>(string documentId, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default) where T : IElasticsearchModal
        {
            Elastic.Clients.Elasticsearch.GetResponse<T> getResponse = await _elasticsearchClient.GetAsync<T>(documentId, index => index
                                                                                                        .Index(indexName), cancellationToken);

            return getResponse.Source;
        }

        /// <summary>
        /// Arama sürecinde bir alandaki metni analiz eder, analiz sonucunda elde edilen terimleri kullanarak aramayı gerçekleştirir.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="queryKeyword"></param>
        /// <param name="indexName"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="from"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<T>> MatchQueryAsync<T>(Expression<Func<T, object>> field, string queryKeyword, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default, int from = 0, int size = 10) where T : IElasticsearchModal
        {
            Elastic.Clients.Elasticsearch.SearchResponse<T> searchResponse = await _elasticsearchClient.SearchAsync<T>(index => index
                                                                                                    .Index(indexName)
                                                                                                    .Query(query => query
                                                                                                        .Match(t => t.Field(field)
                                                                                                                     .Query(queryKeyword)))
                                                                                                    .From(from)
                                                                                                    .Size(size), cancellationToken);

            return searchResponse.Documents;
        }


        /// <summary>
        /// Özellikle arama süreçlerinde kullanıcılar tarafından yanlış yazımdan kaynaklı kelime hatalarının söz konusu olduğu aramalarda benzer kelimeleri bulmak için fuzzy aramasından istifade edebiliriz
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="queryKeyword"></param>
        /// <param name="indexName"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="from"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<T>> FuzzyQueryAsync<T>(Expression<Func<T, object>> field, string queryKeyword, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default, int from = 0, int size = 10) where T : IElasticsearchModal
        {
            Elastic.Clients.Elasticsearch.SearchResponse<T> searchResponse = await _elasticsearchClient.SearchAsync<T>(index => index
                                                                                                    .Index(indexName)
                                                                                                    .Query(query => query
                                                                                                        .Fuzzy(t => t.Field(field)
                                                                                                                     .Value(queryKeyword)))
                                                                                                    .From(from)
                                                                                                    .Size(size), cancellationToken);

            return searchResponse.Documents;
        }

        /// <summary>
        /// Veri sorgulama sürecinde belirli bir kalıba uygun taramada bulunabilmek için yıldız(*) ve soru işareti(?) gibi joker karakterlerden istifade etmekteyiz.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="queryKeyword"></param>
        /// <param name="indexName"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="from"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<T>> WildcardQueryAsync<T>(Expression<Func<T, object>> field, string queryKeyword, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default, int from = 0, int size = 10) where T : IElasticsearchModal
        {
            Elastic.Clients.Elasticsearch.SearchResponse<T> searchResponse = await _elasticsearchClient.SearchAsync<T>(index => index
                                                                                                    .Index(indexName)
                                                                                                    .Query(query => query
                                                                                                        .Wildcard(t => t.Field(field)
                                                                                                                        .Value(queryKeyword)))
                                                                                                    .From(from)
                                                                                                    .Size(size), cancellationToken);

            return searchResponse.Documents;
        }


        /// <summary>
        /// Bool sorgusu, birden fazla sorguyu bir araya getirip mantıksal ilişki ile birleştirmemizi sağlamakta ve böylece karmaşık sorgu yapıları oluşturmamıza olanak tanımaktadır.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matchField"></param>
        /// <param name="matchQueryKeyword"></param>
        /// <param name="fuzzyField"></param>
        /// <param name="fuzzyQueryKeyword"></param>
        /// <param name="wildcardField"></param>
        /// <param name="wildcardQueryKeyword"></param>
        /// <param name="indexName"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="from"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<T>> BoolQueryAsync<T>(Expression<Func<T, object>> matchField, string matchQueryKeyword, Expression<Func<T, object>> fuzzyField, string fuzzyQueryKeyword, Expression<Func<T, object>> wildcardField, string wildcardQueryKeyword, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default, int from = 0, int size = 10) where T : IElasticsearchModal
        {
            Elastic.Clients.Elasticsearch.SearchResponse<T> searchResponse = await _elasticsearchClient.SearchAsync<T>(index => index
                                                                                                    .Index(indexName)
                                                                                                    .Query(query => query
                                                                                                        .Bool(t => t.Should(
                                                                                                            match => match.Match(t => t.Field(matchField).Query(matchQueryKeyword)),
                                                                                                            fuzzy => fuzzy.Fuzzy(p => p.Field(fuzzyField).Value(fuzzyQueryKeyword)),
                                                                                                            wildcard => wildcard.Wildcard(p => p.Field(wildcardField).Value(wildcardQueryKeyword))
                                                                                                            )))
                                                                                                    .From(from)
                                                                                                    .Size(size), cancellationToken);

            return searchResponse.Documents;
        }

        /// <summary>
        /// Eğer ki tam olarak belirtilen ifadeyi eşleştirmek istiyorsak term aramasından istifade edebiliriz.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="queryKeyword"></param>
        /// <param name="indexName"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="from"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<T>> TermQueryAsync<T>(Expression<Func<T, object>> field, string queryKeyword, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default, int from = 0, int size = 10) where T : IElasticsearchModal
        {
            Elastic.Clients.Elasticsearch.SearchResponse<T> searchResponse = await _elasticsearchClient.SearchAsync<T>(index => index
                                                                                            .Index(indexName)
                                                                                            .Query(query => query
                                                                                                .Term(t => t.Field(field)
                                                                                                            .Value(queryKeyword)))
                                                                                            .From(from)
                                                                                            .Size(size), cancellationToken);

            return searchResponse.Documents;
        }


        /// <summary>
        /// Belirli bir alanın var olup olmadığını kontrol eder
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="indexName"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="from"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<T>> ExistsQueryAsync<T>(Expression<Func<T, object>> field, string indexName = ElasticsearchIndexes.DefaultIndex, CancellationToken cancellationToken = default, int from = 0, int size = 10) where T : IElasticsearchModal
        {
            Elastic.Clients.Elasticsearch.SearchResponse<T> searchResponse = await _elasticsearchClient.SearchAsync<T>(index => index
                                                                                            .Index(indexName)
                                                                                            .Query(query => query
                                                                                                .Exists(t => t.Field(field)))
                                                                                            .From(from)
                                                                                            .Size(size), cancellationToken);

            return searchResponse.Documents;
        }
    }
}
