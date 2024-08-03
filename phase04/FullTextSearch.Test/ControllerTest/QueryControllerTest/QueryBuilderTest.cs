using FullTextSearch.Controller.QueryController;
using FullTextSearch.Controller.QueryController.Abstraction;
using NSubstitute;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.QueryTest
{
    public class QueryBuilderTest
    {
        private readonly IQueryFormatter _queryFormatter;
        private readonly IQueryBuilder _sut;

        public QueryBuilderTest()
        {
            _queryFormatter = Substitute.For<IQueryFormatter>();
            _sut = new QueryBuilder(_queryFormatter);
        }

        [Fact]
        public void BuildText_ShouldSetQueryText_WhenGivenText()
        {
            // Arrange
            var text = "Ali is someone !";

            // Act
            _sut.BuildText(text);

            // Assert
            Assert.Equal(text, _sut.GetQuery().Text);
        }

        [Fact]
        public void BuildWordsBySign_ShouldOrganizeWordsCorrectly_WhenGivenSignedWords()
        {
            // Arrange
            var text = "+amir -reza";
            var signs = new List<char> { '+', '-' };

            _queryFormatter.ToUpper(text).Returns("+AMIR -REZA");
            _queryFormatter.Split("+AMIR -REZA", " ").Returns(new List<string> { "+AMIR", "-REZA" });

            _queryFormatter.CollectBySign(
                Arg.Is<IEnumerable<string>>(x => x.SequenceEqual(new List<string> { "+AMIR", "-REZA" })), 
                '+'
            ).Returns(new List<string> { "+AMIR" });

            _queryFormatter.CollectBySign(
                Arg.Is<IEnumerable<string>>(x => x.SequenceEqual(new List<string> { "-REZA" })), 
                '-'
            ).Returns(new List<string> { "-REZA" });

            _queryFormatter.RemovePrefix(
                Arg.Is<IEnumerable<string>>(x => x.SequenceEqual(new List<string> { "+AMIR" }))
            ).Returns(new List<string> { "AMIR" });

            _queryFormatter.RemovePrefix(
                Arg.Is<IEnumerable<string>>(x => x.SequenceEqual(new List<string> { "-REZA" }))
            ).Returns(new List<string> { "REZA" });

            _sut.BuildText(text);

            // Act
            _sut.BuildWordsBySign(signs);
            var query = _sut.GetQuery();

            // Assert
            Assert.Equal(new List<string> { "AMIR" }, query.WordsBySign['+']);
            Assert.Equal(new List<string> { "REZA" }, query.WordsBySign['-']);
        }
    }
}