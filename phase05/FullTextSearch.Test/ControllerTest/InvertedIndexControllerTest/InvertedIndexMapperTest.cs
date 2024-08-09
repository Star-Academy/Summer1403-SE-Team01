using FullTextSearch.Controller.InvertedIndexController;
using FullTextSearch.Controller.InvertedIndexController.Abstraction;
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

            var document1 = documentList[0];
            var document2 = documentList[1];
            var document3 = documentList[2];

            var documents = new List<Document>
            {
                document1, document2, document3
            };

            var expected = DataSample.GetInvertedIndexMap(document1, 
                document2, document3);

            // Act
            var actual = _sut.Map(documents);

            // Assert
            Assert.Equivalent(expected, actual);
        }
    }
}