using FullTextSearch.Controller.QueryController;
using FullTextSearch.Controller.QueryController.Abstraction;
using FullTextSearch.Controller.ResultController;
using FullTextSearch.Controller.ResultController.Abstraction;
using FullTextSearch.Controller.SearchController;
using FullTextSearch.Core;

namespace FullTextSearch.Service.SearchService;

public class SearchService : ISearchService
{
    private readonly IQueryBuilder _queryBuilder;
    private readonly IResultBuilder _resultBuilder;


    public SearchService(IQueryBuilder queryBuilder, IResultBuilder resultBuilder)
    {
        _queryBuilder = queryBuilder;
        _resultBuilder = resultBuilder;
    }

    public Result Search(string input, Dictionary<string, IEnumerable<Document>> invertedIndex)
    {
        //QueryBuilder queryBuilder = new QueryBuilder(new QueryFormatter());
        new QueryDirector().Construct(input, new List<char>(){'+', '-'}, _queryBuilder);
        var query = _queryBuilder.GetQuery();

        //ResultBuilder resultBuilder = new ResultBuilder(new FilterDriver(), new SearcherDriver());
        new ResultDirector().Construct(_resultBuilder, query, invertedIndex);
        var result = _resultBuilder.GetResult();
        
        return result;
    }
}