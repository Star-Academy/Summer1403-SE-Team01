using FullTextSearch.Controller.QueryController.Abstraction;
using FullTextSearch.Controller.ResultController.Abstraction;
using FullTextSearch.Core;
using FullTextSearch.Service.SearchService;
using FullTextSearch.Test.Data;
using NSubstitute;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ServiceTest.SearchServiceTest;

public class SearchServiceTest
{
    private readonly IQueryBuilder _queryBuilder;
    private readonly IResultBuilder _resultBuilder;
    private readonly ISearchService _sut;

    public SearchServiceTest()
    {
        _queryBuilder = Substitute.For<IQueryBuilder>();
        _resultBuilder = Substitute.For<IResultBuilder>();
        _sut = new SearchService(_queryBuilder, _resultBuilder);
    }

    [Test]
    public void Search_ShouldReturnResultForPassesQuery()
    {
        // Arrange
        string input = "cat +reza -demand";
        IEnumerable<char> signs = new List<char>() {'+', '-'};

        var query = new Query();
        query.Text = input;
        query.WordsBySign = new Dictionary<char, IEnumerable<string>>()
        {
            {'+', new List<string>() {"reza"}},
            {'-', new List<string>() {"demand"}},
            {' ', new List<string>() {"cat"}}
        };
        
        var documentList = DataSample.GetDocuments();

        Document document1 = documentList[0];
        Document document2 = documentList[1];
        Document document3 = documentList[2];
        
        var expected = new Result();
        expected.documents = new List<Document>() { document1 };
        expected.documentsBySign = new Dictionary<char, IEnumerable<Document>>
        {
            { '+', new List<Document> { document1, document2} },
            { '-', new List<Document> { document2} },
            { ' ', new List<Document> { document1, document2, document3 } }
        };

        var invertedIndexMap = DataSample.GetInvertedIndexMap(document1, 
            document2, document3);


        _queryBuilder.GetQuery().Returns(query);
        _resultBuilder.GetResult().Returns(expected);
        
        // Act
        var actual = _sut.Search(input, invertedIndexMap);
        
        // Assert
        Assert.Equal(actual, expected);
    }
}