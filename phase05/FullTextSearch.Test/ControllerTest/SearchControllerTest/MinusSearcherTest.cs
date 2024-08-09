using FullTextSearch.Controller.SearchController;
using FullTextSearch.Controller.SearchController.Abstraction;
using FullTextSearch.Core;
using FullTextSearch.Test.Data;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ControllerTest.SearchControllerTest;

public class MinusSearcherTest
{
    private readonly ISearcher _sut;

    public MinusSearcherTest()
    {
        _sut = new MinusSearcher();
    }

    [Fact]
    public void Search_ShouldReturnDocumentsExcludingMinusWords_WhenGivenQueryAndInvertedIndex()
    {
        // Arrange
        var query = new Query();
        query.Text = "cat +reza -demand!";

        query.WordsBySign = new Dictionary<char, IEnumerable<string>>()
        {
            { '+', new List<string>() { "reza" } },
            { '-', new List<string>() { "demand" } },
            { ' ', new List<string>() { "cat" } },
        };

        var documentList = DataSample.GetDocuments();

        var document1 = documentList[0];
        var document2 = documentList[1];
        var document3 = documentList[2];
        
        var invertedIndexMap = DataSample.GetInvertedIndexMap(document1, 
            document2, document3);
        
        var expected = new List<Document>() {};

        // Act
        var actual = _sut.Search(query, invertedIndexMap);
        
        // Assert
        Assert.Equivalent(expected, actual);
    }
}