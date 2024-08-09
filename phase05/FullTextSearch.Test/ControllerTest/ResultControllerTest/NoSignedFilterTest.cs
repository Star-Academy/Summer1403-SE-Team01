using FullTextSearch.Controller.ResultController;
using FullTextSearch.Controller.ResultController.Abstraction;
using FullTextSearch.Core;
using FullTextSearch.Test.Data;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ControllerTest.ResultControllerTest;

public class NoSignedFilterTest
{
    private readonly IFilter _sut;

    public NoSignedFilterTest()
    {
        _sut = new NoSignedFilter();
    }
    [Fact]
    public void Filter_ShouldReturnDocumentsWithoutSignedWords_WhenDocumentsContainNoSignedWords()
    {
        // Arrange
        var documentList = DataSample.GetDocuments();

        var document1 = documentList[0];
        var document2 = documentList[1];
        var document3 = documentList[2];

        
        var documents = new List<Document>() { document1, document2, document3 };
        
        var documentsBySign = new Dictionary<char, IEnumerable<Document>>
        {
            { '+', new List<Document> { document1, document2 } },
            { '-', new List<Document> { document1, document2, document3 } },
            { ' ', new List<Document> { document3 } }
        };
        var expected = documents.Intersect(documentsBySign[' ']);
        
        // Act
        var actual = _sut.Filter(documents, documentsBySign);

        // Assert
        Assert.Equivalent(expected, actual);
    }
}