using System.Text.RegularExpressions;
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
        public void ToUpper_ShouldReturnUppercaseVersionOfText(string expected, string text)
        {
            Assert.Equal(expected, _sut.ToUpper(text));
        }

        [Xunit.Theory]
        [InlineData(new[] { "salAm!" }, "salAm!", " ")]
        [InlineData(new[] { "A", "L", "I" }, "A L I", " ")]
        public void Split_ShouldReturnSplitTextArray(string[] expectedArray, string text, string regex)
        {
            var expected = new List<string>(expectedArray);
            var result = _sut.Split(text, regex);
            Assert.Equal(expected, result);
        }
        
        [Xunit.Theory]
        [InlineData(new[] { "+ali", "+mohammad" }, new[] { "+ali", "-reza", "zahra", "+mohammad" }, '+')]
        [InlineData(new[] { "-reza" }, new[] { "+ali", "-reza", "zahra", "+mohammad" }, '-')]
        [InlineData(null, new[] { "+ali", "-reza", "zahra", "+mohammad" }, '*')]
        public void CollectBySign_ShouldReturnWordsWithSpecifiedSign(string[] expectedArray, string[] listArray, char c)
        {
            var list = new List<string>(listArray);
            var expected = expectedArray == null ? new List<string>() : new List<string>(expectedArray);
            var result = _sut.CollectBySign(list, c).ToList();
            Assert.Equal(expected, result);
        }

        [Xunit.Theory]
        [InlineData(new[] { "ali", "reza", "zahra", "mohammad" }, new[] { "+ali", "+reza", "+zahra", "+mohammad" })]
        [InlineData(new[] { "ali", "reza", "zahra", "mohammad" }, new[] { "+ali", "-reza", "!zahra", "+mohammad" })]
        public void RemovePrefix_ShouldReturnWordsWithoutPrefix(string[] expectedArray, string[] listArray)
        {
            var list = new List<string>(listArray);
            var expected = new List<string>(expectedArray);
            var result = _sut.RemovePrefix(list).ToList();
            Assert.Equal(expected, result);
        }
    }
}
