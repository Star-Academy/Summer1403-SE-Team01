using FullTextSearch.Controller.QueryController;
using FullTextSearch.Controller.ResultController;
using FullTextSearch.Controller.SearchController;
using FullTextSearch.Core;
using FullTextSearch.Service.SearchService.Abstraction;

namespace FullTextSearch.Service.SearchService;

public class SearchService : ISearchService
{
    public Result Search(string input, Dictionary<string, IEnumerable<Document>> invertedIndex)
    {
        QueryBuilder queryBuilder = new QueryBuilder(new WordCollectorDriver());
        new QueryDirector().Construct(input, queryBuilder);
        var query = queryBuilder.GetQuery();

        ResultBuilder resultBuilder = new ResultBuilder(new FilterDriver(), new SearcherDriver());
        new ResultDirector().Construct(resultBuilder, query, invertedIndex);
        var result = resultBuilder.GetResult();
        
        return result;
    }
}