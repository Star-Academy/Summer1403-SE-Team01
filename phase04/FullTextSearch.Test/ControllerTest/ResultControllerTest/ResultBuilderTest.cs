using FullTextSearch.Controller.ResultController;
using FullTextSearch.Controller.ResultController.Abstraction;
using FullTextSearch.Controller.SearchController;
using FullTextSearch.Controller.SearchController.Abstraction;
using FullTextSearch.Core;
using NSubstitute;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ControllerTest.ResultControllerTest;

public class ResultBuilderTest
{
    private readonly ISearcherDriver _searcherDriver;
    private readonly IFilterDriver _filterDriver;
    private readonly ResultBuilder _sut;

    public ResultBuilderTest()
    {
        _searcherDriver = Substitute.For<ISearcherDriver>();
        _filterDriver = Substitute.For<IFilterDriver>();
        _sut = new ResultBuilder(_filterDriver, _searcherDriver);
    }

    [Fact]
    public void BuildDocumentsBySign_ShouldFillDocumentsBySign_WhenGivenSearchersAndQuery()
    {
        // Arrange
        IEnumerable<ISearcher> searchers = new List<ISearcher>() {new MinusSearcher(), new PlusSearcher(), new NoSignedSearcher()};
        Query query = new Query();
        query.Text = "cat +reza -demand";
        query.WordsBySign = new Dictionary<char, IEnumerable<string>>()
        {
            {'+', new List<string>() {"reza"}},
            {'-', new List<string>() {"demand"}},
            {' ', new List<string>() {"cat"}}
        };
        Result result = new Result();
        
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

        var invertedIndex = new Dictionary<string, IEnumerable<Document>>
        {
            { "cat", new List<Document> { document1, document2, document3} },
            { "reza", new List<Document> {document1, document2} },
            { "demand", new List<Document> { document2 } }
        };
        
        _searcherDriver.DriveSearch(searchers, query, result, invertedIndex);
        
        // Act
        _sut.BuildDocumentsBySign(searchers, query, invertedIndex);
        
        // Assert
        Assert.Equal(result.documentsBySign.Keys.Count, _sut.GetResult().documentsBySign.Keys.Count);
        foreach (var entry in _sut.GetResult().documentsBySign)
        {
            Assert.True(entry.Value.SequenceEqual(result.documentsBySign[entry.Key]));
        }
    }
    
    [Test]
    public void BuildDocuments_ShouldFillResultDocuments()
    {
        // Arrange
        IEnumerable<IFilter> filters = new List<IFilter>() {new NoSignedFilter(), new MinusFilter(), new PlusFilter()};
        Result result = new Result();
        
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

        var invertedIndex = new Dictionary<string, IEnumerable<Document>>
        {
            { "cat", new List<Document> { document1, document2, document3} },
            { "reza", new List<Document> {document1, document2} },
            { "demand", new List<Document> { document2 } }
        };
        
        result.documents = new UniversalSearch().GetUniversal(invertedIndex);
        _filterDriver.DriveFilterer(filters, result);      

        // Act
        _sut.BuildDocuments(filters, invertedIndex);
        
        // Assert
        Assert.True(result.documents.SequenceEqual(_sut.GetResult().documents));
    }
}
