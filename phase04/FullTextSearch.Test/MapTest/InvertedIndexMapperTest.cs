using FullTextSearch.Controller.InvertedIndexController;
using FullTextSearch.Core;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test
{
    public class InvertedIndexMapperTests
    {
        private readonly InvertedIndexMapper _sut;

        public InvertedIndexMapperTests()
        {
            _sut = new InvertedIndexMapper();
        }
        [Fact]
        public void Map_ShouldReturnCorrectInvertedIndex()
        {
            // Arrange
            Document document1 = new Document
            {
                Name = "Doc1",
                Path = "./ResourcesTest/Doc1",
                Text = "reza ali mohammad hello",
                Words = new List<string> { "reza", "ali", "hello" }
            };
            
            Document document2 = new Document
            {
                Name = "Doc2",
                Path = "./ResourcesTest/Doc2",
                Text = "reza ali mohammad hello",
                Words = new List<string> { "reza", "mohammad", "hello" }
            };
            
            Document document3 = new Document
            {
                Name = "Doc3",
                Path = "./ResourcesTest/Doc3",
                Text = "reza ali mohammad hello",
                Words = new List<string> { "reza", "ali", "mohammad" }
            };

            var documents = new List<Document> { document1, document2, document3 };

            var expected = new Dictionary<string, IEnumerable<Document>>
            {
                { "reza", new List<Document> { document1, document2, document3 } },
                { "ali", new List<Document> { document1, document3 } },
                { "hello", new List<Document> { document1, document2 } },
                { "mohammad", new List<Document> { document2, document3 } }
            };

            var mapper = new InvertedIndexMapper();

            // Act
            var result = mapper.Map(documents);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expected.Count, result.Count);

            foreach (var entry in expected)
            {
                Assert.True(result.ContainsKey(entry.Key));
                Assert.True(entry.Value.SequenceEqual(result[entry.Key]));
            }
        }
    }
}
