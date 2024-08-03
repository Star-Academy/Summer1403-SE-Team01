using FullTextSearch.Controller.InvertedIndexController;
using FullTextSearch.Core;
using FullTextSearch.Test.Data;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ControllerTest.InvertedIndexControllerTest
{
    public class InvertedIndexMapperTests
    {
        private readonly IInvertedIndexMapper _sut;
        
        public InvertedIndexMapperTests()
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

            var expected = DataSample.GetInvertedIndexMap(document1, 
                document2, document3);

            // Act
            var result = _sut.Map(documents);

            // Assert
            Assert.NotNull(expected);
            Assert.Equal(expected.Keys.Count, result.Keys.Count);
            foreach (var entry in expected)
            {
                Assert.True(expected[entry.Key].SequenceEqual(result[entry.Key]));
            }
        }
    }
}