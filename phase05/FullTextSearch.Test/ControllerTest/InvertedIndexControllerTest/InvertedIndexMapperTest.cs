using System.Collections.Generic;
using System.Linq;
using Xunit;
using FullTextSearch.Controller;
using FullTextSearch.Controller.InvertedIndexController;
using FullTextSearch.Core;
using FullTextSearch.Test.Data;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ControllerTest
{
    public class InvertedIndexMapperTests
    {
        private readonly IInvertedIndexMapper _sut;

        public InvertedIndexMapperTests()
        {
            _sut = new InvertedIndexMapper(); // Assuming this is the correct implementation
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
            Assert.NotNull(result);
            Assert.NotNull(expected);
            Assert.Equal(expected.Count, result.Count);

            // Check if both dictionaries have the same keys
            foreach (var key in expected.Keys)
            {
                Assert.True(result.TryGetValue(key, out var resultValues), $"Key '{key}' not found in the result dictionary.");

                var expectedValuesSet = new HashSet<Document>(expected[key]);
                var resultValuesSet = new HashSet<Document>(resultValues);

                // Assert that both sets contain the same elements
                Assert.Equal(expectedValuesSet, resultValuesSet);
            }
        }
    }
}