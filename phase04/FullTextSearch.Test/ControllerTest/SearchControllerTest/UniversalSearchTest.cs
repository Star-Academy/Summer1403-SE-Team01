using FullTextSearch.Controller.SearchController;
using FullTextSearch.Core;
using FullTextSearch.Test.Data;
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
        var documentList = DataSample.GetDocuments();

        Document document1 = documentList[0];
        Document document2 = documentList[1];
        Document document3 = documentList[2];
            
        var invertedIndexMap = DataSample.GetInvertedIndexMap(document1, 
            document2, document3);

            
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