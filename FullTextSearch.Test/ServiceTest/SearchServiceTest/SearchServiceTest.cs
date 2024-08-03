using FullTextSearch.Controller.QueryController;
using FullTextSearch.Controller.QueryController.Abstraction;
using FullTextSearch.Controller.ResultController;
using FullTextSearch.Controller.ResultController.Abstraction;
using FullTextSearch.Core;
using FullTextSearch.Service.SearchService;
using NSubstitute;

namespace FullTextSearch.Test;

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
    public void SearchTest()
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
        
        Document document1 = new Document();
        document1.Name = "Doc1";
        document1.Path = "./ResourcesTest/Doc1";
        document1.Text = "reza ali mohammad hello";
        document1.Words = new List<string> {"cat", "reza", "hello"};
            
            
        Document document2 = new Document();
        document2.Name = "Doc2";
        document2.Path = "./ResourcesTest/Doc2";
        document2.Text = "reza ali mohammad hello";
        document2.Words = new List<string> {"cat", "reza", "demand"};
            
            
        Document document3 = new Document();
        document3.Name = "Doc3";
        document3.Path = "./ResourcesTest/Doc3";
        document3.Text = "reza ali mohammad hello";
        document3.Words = new List<string> {"cat", "ali", "hello"};

        
        var expected = new Result();
        expected.documents = new List<Document>() { document1 };
        expected.documentsBySign = new Dictionary<char, IEnumerable<Document>>
        {
            { '+', new List<Document> { document1, document2} },
            { '-', new List<Document> { document2} },
            { ' ', new List<Document> { document1, document2, document3 } }
        };
        
        var invertedIndex = new Dictionary<string, IEnumerable<Document>>
        {
            { "cat", new List<Document> { document1, document2, document3} },
            { "reza", new List<Document> {document1, document2} },
            { "demand", new List<Document> { document2 } }
        };

        //new QueryDirector().Construct(input, signs, _queryBuilder); // no need to these
        _queryBuilder.GetQuery().Returns(query);

        //ResultBuilder resultBuilder = new ResultBuilder(new FilterDriver(), new SearcherDriver());
        //new ResultDirector().Construct(_resultBuilder, query, invertedIndex);
        _resultBuilder.GetResult().Returns(expected);
        
        // Act
        var result = _sut.Search(input, invertedIndex);
        
        // Assert
        Xunit.Assert.Equal(result, expected);
    }
}