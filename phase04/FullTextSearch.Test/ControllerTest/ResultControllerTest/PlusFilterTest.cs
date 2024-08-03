using FullTextSearch.Controller.ResultController;
using FullTextSearch.Controller.ResultController.Abstraction;
using FullTextSearch.Core;
using FullTextSearch.Test.Data;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ControllerTest.ResultControllerTest;

public class PlusFilterTest
{
    private readonly IFilter _sut;

    public PlusFilterTest()
    {
        _sut = new PlusFilter();
    }
    [Fact]
    public void Filter_ShouldReturnDocumentsContainingPlusWords_WhenFilteringWithPlusSign()
    {
        // Arrange
        var documentList = DataSample.GetDocuments();

        Document document1 = documentList[0];
        Document document2 = documentList[1];
        Document document3 = documentList[2];

        
        IEnumerable<Document> documents = new List<Document>() { document1, document2, document3 };
        
        var documentsBySign = new Dictionary<char, IEnumerable<Document>>
        {
            { '+', new List<Document> { document1, document2 } },
            { '-', new List<Document> { document1, document2, document3 } },
            { ' ', new List<Document> { document3 } }
        };
        var expected = documents.Intersect(documentsBySign['+']);
        
        // Act
        var actual = _sut.Filter(documents, documentsBySign);

        // Assert
        Assert.True(actual.SequenceEqual(expected));
    } 
}