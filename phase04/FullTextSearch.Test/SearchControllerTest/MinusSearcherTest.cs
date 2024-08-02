using FullTextSearch.Controller.SearchController;
using FullTextSearch.Core;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.SearchControllerTest;

public class MinusSearcherTest
{
    private readonly ISearcher _sut;

    public MinusSearcherTest()
    {
        _sut = new MinusSearcher();
    }

    [Fact]
    public void Search_WithValidQuery_ReturnsExpectedDocuments()
    {
        
        //Arrange
        
        Document document1 = new Document
        {
            Name = "Doc1",
            Path = "./ResourcesTest/Doc1",
            Text = "ali mohammad hello",
            Words = new List<string> { "ali", "hello" }
        };
                
        Document document2 = new Document
        {
            Name = "Doc2",
            Path = "./ResourcesTest/Doc2",
            Text = "reza ali mohammad hello",
            Words = new List<string> { "reza", "mohammad", "hello" }
        };
                
        Document document3 = new Document
        {
            Name = "Doc3",
            Path = "./ResourcesTest/Doc3",
            Text = "reza ali mohammad hello",
            Words = new List<string> { "reza", "ali", "mohammad" }
        };
        
        var invertedIndexMap = new Dictionary<string, IEnumerable<Document>>
        {
            { "reza", new List<Document> { document2, document3 } },
            { "ali", new List<Document> { document1, document3 } },
            { "hello", new List<Document> { document1, document2 } },
            { "mohammad", new List<Document> { document2, document3 } }
        };
        
        Query query = new Query();
        query.Text = "+salam -reza";
        query.WordsBySign.Add('-', new []{"reza"});
        query.WordsBySign.Add('+', new []{"salam"});
        
        //Act
        
        var result = _sut.Search(query, invertedIndexMap).ToList();
        
        // Assert
        
        Assert.Contains(document2, result); 
        Assert.Contains(document3, result);
        Assert.Equal(2, result.Count);
    }
}