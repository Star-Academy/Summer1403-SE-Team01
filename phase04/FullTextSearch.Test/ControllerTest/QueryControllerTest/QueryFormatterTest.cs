using System.Collections;
using FullTextSearch.Controller.QueryController;
using Xunit;
using Assert = Xunit.Assert;

namespace InvertedIndex.Test.Query
{
    public class QueryFormatterTest
    {
        private readonly QueryFormatter _sut;

        public QueryFormatterTest()
        {
            _sut = new QueryFormatter();
        }

        [Xunit.Theory]
        [InlineData("SALAM!", "salAm!")]
        [InlineData("ALI", "ALI")]
        [InlineData("REZA", "reza")]
        public void ToUpper_ShouldReturnUppercaseVersionOfText_WhenGivenText(string expected, string text)
        {
            // Arrange

            // Act
            var result = _sut.ToUpper(text);

            // Assert
            Assert.Equal(expected, result);
        }

        [Xunit.Theory]
        [InlineData("This is a test.", " ", new[] { "This", "is", "a", "test." })]
        [InlineData("amir!", " ", new[] { "amir!" })]
        public void Split_ShouldReturnExpectedResults_WhenGivenTextAndDelimiter(string queryText, string regex, string[] expected)
        {
            // Arrange

            // Act
            var result = _sut.Split(queryText, regex);

            // Assert
            Assert.Equal(expected, result);
        }

        [Xunit.Theory]
        [MemberData(nameof(CollectBySignTestData.Data), MemberType = typeof(CollectBySignTestData))]
        public void CollectBySign_ShouldReturnWordsWithSpecifiedSign_WhenGivenListAndSign(string[] expectedArray, string[] listArray, char c)
        {
            // Arrange
            var list = new List<string>(listArray);
            var expected = expectedArray == null ? new List<string>() : new List<string>(expectedArray);

            // Act
            var result = _sut.CollectBySign(list, c).ToList();

            // Assert
            Assert.Equal(expected, result);
        }

        [Xunit.Theory]
        [ClassData(typeof(RemovePrefixTestData))]
        public void RemovePrefix_ShouldReturnWordsWithoutPrefix_WhenGivenList(string[] expectedArray, string[] listArray)
        {
            // Arrange
            var list = new List<string>(listArray);
            var expected = new List<string>(expectedArray);

            // Act
            var result = _sut.RemovePrefix(list).ToList();

            // Assert
            Assert.Equal(expected, result);
        }
    }

    public class CollectBySignTestData : IEnumerable<object[]>
    {
        public static IEnumerable<object[]> Data => new List<object[]>
        {
            new object[] { new[] { "+ali", "+mohammad" }, new[] { "+ali", "-reza", "zahra", "+mohammad" }, '+' },
            new object[] { new[] { "-reza" }, new[] { "+ali", "-reza", "zahra", "+mohammad" }, '-' },
            new object[] { null, new[] { "+ali", "-reza", "zahra", "+mohammad" }, '*' }
        };

        public IEnumerator<object[]> GetEnumerator() => Data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class RemovePrefixTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new[] { "ali", "reza", "zahra", "mohammad" }, new[] { "+ali", "+reza", "+zahra", "+mohammad" } };
            yield return new object[] { new[] { "ali", "reza", "zahra", "mohammad" }, new[] { "+ali", "-reza", "!zahra", "+mohammad" } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}