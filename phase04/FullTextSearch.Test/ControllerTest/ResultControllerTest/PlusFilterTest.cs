using FullTextSearch.Controller.ResultController;
using FullTextSearch.Controller.ResultController.Abstraction;
using FullTextSearch.Core;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ControllerTest.ResultControllerTest;

public class PlusFilterTest
{
    private readonly IFilter _sut = new PlusFilter();

    [Test]
    public void Filter_ShouldReturnFilteredDocumentsContainingPlusWordsFromResultDocuments()
    {
        // Arrange
        var d1 = new Document();
        var d2 = new Document();
        var d3 = new Document();
        var d4 = new Document();
        var d5 = new Document();
        
        IEnumerable<Document> documents = new List<Document>() {d1, d2, d3};
        Dictionary<char, IEnumerable<Document>> documentsBySign = new Dictionary<char, IEnumerable<Document>>()
        {
            {'+', new List<Document>(){d1, d2}},
            {'-', new List<Document>(){d1, d2, d5}},
            {' ', new List<Document>(){d5, d4}}
        };
        var expected = documents.Intersect(documentsBySign['+']);
        
        // Act
        var actual = _sut.Filter(documents, documentsBySign);

        // Assert
        Assert.True(actual.SequenceEqual(expected));
    } 
}