using FullTextSearch.Controller.InvertedIndexController;
using FullTextSearch.Core;
using FullTextSearch.Test.Data;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ControllerTest.InvertedIndexControllerTest
{
    public class NewInvertedIndexMapTests
    {
        private readonly IInvertedIndexMapper _sut;
        
        public NewInvertedIndexMapTests()
        {
            _sut = new InvertedIndexMapper();
        }

        [Fact]
        public void Map_ShouldReturnCorrectInvertedIndex_WhenGivenDocuments()
        {
            // Arrange
            var documentList = DataSample.GetDocuments();

            Document document1 = documentList[0];
            Document document2 = documentList[1];
            Document document3 = documentList[2];
            
            var documents = new List<Document>
            {
                document1, document2, document3
            };

            var expected = new Dictionary<string, List<Document>>
            {
                { "reza", new List<Document> { documents[0], documents[1], documents[2]} },
                {"reza ali", new List<Document>(){documents[0], documents[1], documents[2]}},
                {"reza ali hello", new List<Document>(){documents[0]}},
                { "ali", new List<Document> { documents[0], documents[2] } },
                { "ali hello", new List<Document> { documents[0] } },
                {"hello", new List<Document>(){documents[0]}},
                {"mohammad", new List<Document>(){documents[1], documents[2]}},
                {"mohammad reza", new List<Document>(){documents[1], documents[2]}},
                {"mohammad reza ali", new List<Document>(){documents[1], documents[2]}}
            };
            
            //var expected = DataSample.GetInvertedIndexMap(document1, 
             //   document2, document3);

            // Act
            var actual = _sut.Map(documents);

            // Assert
            Assert.NotNull(expected);
            Assert.Equal(expected.Keys.Count, actual.Keys.Count);
            foreach (var entry in expected)
            {
                Assert.True(expected[entry.Key].SequenceEqual(actual[entry.Key]));
            }
        }
    }
}