using FullTextSearch.Controller.QueryController;
using FullTextSearch.Controller.ResultController;
using FullTextSearch.Controller.SearchController;
using FullTextSearch.Core;
using FullTextSearch.Service.SearchService;
using FullTextSearch.Service.SearchService.Abstraction;
using FullTextSearch.Test.Data;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ServiceTest.SearchServiceTest;


public class SearchServiceTest
{
    
    private readonly ISearchService _sut;

    public SearchServiceTest()
    {
        _sut = new SearchService();
    }

    [Test]
    public void Search_ShouldReturnResultForPassesQuery()
    {
        // Arrange
        var text = "+ali -hassan \"karim zahra\" +\"ali mohammad\" kabir hoda -leila"; 

        var documentList = DataSample.GetDocuments();

        Document document1 = documentList[0];
        Document document2 = documentList[1];
        Document document3 = documentList[2];
        var invertedIndexMap = DataSample.GetInvertedIndexMap(document1, 
            document2, document3);

        QueryBuilder queryBuilder = new QueryBuilder(new WordCollectorDriver());
        new QueryDirector().Construct(text, queryBuilder);
        var query = queryBuilder.GetQuery();

        ResultBuilder resultBuilder = new ResultBuilder(new FilterDriver(), new SearcherDriver());
        new ResultDirector().Construct(resultBuilder, query, invertedIndexMap);
        var expected = resultBuilder.GetResult();

        // Act
        var actual = _sut.Search(text, invertedIndexMap);
        
        // Assert
        Assert.Equivalent(expected, actual);
    }
}