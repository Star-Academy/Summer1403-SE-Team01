using FullTextSearch.Controller.SearchController;
using FullTextSearch.Core;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.SearchControllerTest;

public class UniversalSearchTest
{
    private readonly IUniversalSearch _sut;

    public UniversalSearchTest()
    {
        _sut = new UniversalSearch();
    }
    
    [Fact]
    public void GetUniversal_ShouldReturnAllDocuments_WhenGivenInvertedIndex()
    {
        // Arrange
        Document document1 = new Document
        {
            Name = "Doc1",
            Path = "./ResourcesTest/Doc1",
            Text = "reza ali mohammad hello",
            Words = new List<string> { "reza", "ali", "hello" }
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
            { "reza", new List<Document> { document1, document2, document3 } },
            { "ali", new List<Document> { document1, document3 } },
            { "hello", new List<Document> { document1, document2 } },
            { "mohammad", new List<Document> { document2, document3 } }
        };
            
        var expectedDocuments = new List<Document> { document1, document2, document3 };

        // Act
        var result = _sut.GetUniversal(invertedIndexMap).ToList();

        // Assert
        Assert.Equal(expectedDocuments.Count, result.Count);
        foreach (var doc in expectedDocuments)
        {
            Assert.Contains(result, d => d.Equals(doc));
        }
    }
    
    [Fact]
    public void GetUniversal_ShouldReturnEmptyList_WhenGivenEmptyIndex()
    {
        // Arrange
        var invertedIndexMap = new Dictionary<string, IEnumerable<Document>>();

        // Act
        var result = _sut.GetUniversal(invertedIndexMap);

        // Assert
        Assert.Empty(result);
    }
}