using FullTextSearch.Controller.QueryController;
using FullTextSearch.Controller.ResultController;
using FullTextSearch.Controller.SearchController;
using FullTextSearch.Core;

namespace FullTextSearch.Service.SearchService;

public class SearchService : ISearchService
{

    public Result Search(string input, Dictionary<string, IEnumerable<Document>> invertedIndex)
    {
        QueryBuilder queryBuilder = new QueryBuilder(new QueryFormatter());
        new QueryDirector().Construct(input, new List<char>(){'+', '-'}, queryBuilder);
        var query = queryBuilder.GetQuery();

        ResultBuilder resultBuilder = new ResultBuilder(new FiltererDriver(), new SearcherDriver(), query, invertedIndex);
        new ResultDirector().Construct(resultBuilder);
        var result = resultBuilder.GetResult();
        
        return result;
    }
}