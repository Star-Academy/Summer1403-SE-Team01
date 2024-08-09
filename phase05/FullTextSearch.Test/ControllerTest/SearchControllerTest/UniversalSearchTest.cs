using FullTextSearch.Controller.SearchController;
using FullTextSearch.Core;
using FullTextSearch.Test.Data;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ControllerTest.SearchControllerTest;

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

        var document1 = documentList[0];
        var document2 = documentList[1];
        var document3 = documentList[2];
            
        var invertedIndexMap = DataSample.GetInvertedIndexMap(document1, 
            document2, document3);

            
        var expected = new List<Document> { document1, document2, document3 };

        // Act
        var actual = _sut.GetUniversal(invertedIndexMap).ToList();

        // Assert
        Assert.Equivalent(expected, actual);
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