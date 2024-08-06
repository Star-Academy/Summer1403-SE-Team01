using FullTextSearch.Controller.SearchController;
using FullTextSearch.Core;
using FullTextSearch.Test.Data;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ControllerTest.SearchControllerTest;

public class PlusSearcherTest
{
    private readonly ISearcher _sut;

    public PlusSearcherTest()
    {
        _sut = new PlusSearcher();
    }
    [Fact]
    public void Search_ShouldReturnDocumentsContainingPlusWords_WhenGivenQueryAndInvertedIndex()
    {
        // Arrange
        Query query = new Query();
        query.Text = "cat +reza -demand!";
        
        query.WordsBySign = new Dictionary<char, IEnumerable<string>>()
        {
            {'+', new List<string>(){"reza"}},
            {'-', new List<string>(){"demand"}},
            {' ', new List<string>(){"cat"}},
        };

        var documentList = DataSample.GetDocuments();

        Document document1 = documentList[0];
        Document document2 = documentList[1];
        Document document3 = documentList[2];

        var invertedIndexMap = DataSample.GetInvertedIndexMap(document1, 
            document2, document3);

        var expected = new List<Document>() {document1, document2, document3};

        // Act
        var actual = _sut.Search(query, invertedIndexMap);
        
        // Assert
        Assert.True(actual.SequenceEqual(expected));
    }
}