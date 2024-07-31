using FullTextSearch.Controller.InvertedIndexController;
using FullTextSearch.Core;
using Xunit;
namespace FullTextSearch.Test
{
    public class InvertedIndexMapperTests
    {
        [Test]
        public void Map_ShouldReturnCorrectIvertedIndex()
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
                { "reza", new List<Document> { documents[0], documents[1]} },
                { "mohammad", new List<Document> { documents[1], documents[2] } },
                { "ali", new List<Document> { documents[0], documents[2] } },
                { "hello", new List<Document> { documents[0], documents[1] } },
                { "amir", new List<Document> { } }
            };
            var mapper = new InvertedIndexMapper();

            // Act
            var result = mapper.Map(documents);

            // Assert
            Xunit.Assert.NotNull(expected);
            Xunit.Assert.Equal(expected.Count, result.Count);
            foreach (var entry in expected)
            {
                Xunit.Assert.True(result.ContainsKey(entry.Key));
                Xunit.Assert.True(expected[entry.Key].SequenceEqual(result[entry.Key]));
            }
        }
    }
}