using FullTextSearch.Controller.ResultController;
using FullTextSearch.Controller.ResultController.Abstraction;
using FullTextSearch.Core;

namespace FullTextSearch.Test.ControllerTest.ResultTest;

public class MinusFilterTest
{
    private readonly IFilter _sut = new MinusFilter();

    [Test]
    public void FilterTest()
    {
        // Arange
        var d1 = new Document();
        var d2 = new Document();
        var d3 = new Document();
        var d4 = new Document();
        var d5 = new Document();
        
        IEnumerable<Document> documents = new List<Document>() {d1, d2, d3};
        Dictionary<char, IEnumerable<Document>> documentsBySign = new Dictionary<char, IEnumerable<Document>>();
        documentsBySign.Add('+', new List<Document>(){d1, d2});
        documentsBySign.Add('-', new List<Document>(){d1, d2, d5});
        documentsBySign.Add(' ', new List<Document>(){d5, d4});

        var expected = documents.Except(documentsBySign['-']);
        
        // Act
        var result = _sut.Filter(documents, documentsBySign);

        // Assert
        Xunit.Assert.True(result.SequenceEqual(expected));
    }
}