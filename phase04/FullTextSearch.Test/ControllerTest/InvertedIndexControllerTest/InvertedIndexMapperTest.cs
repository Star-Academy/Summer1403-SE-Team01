using FullTextSearch.Controller.InvertedIndexController;
using FullTextSearch.Core;
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

        [Test]
        public void Map_ShouldReturnCorrectInvertedIndex()
        {
            // Arrange
            Document document1 = new Document();
            document1.Name = "Doc1";
            document1.Path = "./ResourcesTest/Doc1";
            document1.Text = "reza ali mohammad hello";
            document1.Words = new List<string> {"reza", "ali", "hello"};
            
            
            Document document2 = new Document();
            document2.Name = "Doc2";
            document2.Path = "./ResourcesTest/Doc2";
            document2.Text = "reza ali mohammad hello";
            document2.Words = new List<string> {"reza", "mohammad", "hello"};
            
            
            Document document3 = new Document();
            document3.Name = "Doc3";
            document3.Path = "./ResourcesTest/Doc3";
            document3.Text = "reza ali mohammad hello";
            document3.Words = new List<string> {"reza", "ali", "mohammad"};
            
            var documents = new List<Document>
            {
                document1, document2, document3
            };

            var expected = new Dictionary<string, List<Document>>
            {
                { "reza", new List<Document> { documents[0], documents[1], documents[2]} },
                { "mohammad", new List<Document> { documents[1], documents[2] } },
                { "ali", new List<Document> { documents[0], documents[2] } },
                { "hello", new List<Document> { documents[0], documents[1] } },
            };

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