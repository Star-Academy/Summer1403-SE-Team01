using FullTextSearch.Controller.QueryController;
using FullTextSearch.Controller.QueryController.Abstraction;
using NSubstitute;
using Xunit;
using Assert = Xunit.Assert;
using System.Collections.Generic;

namespace FullTextSearch.Test.QueryTest
{
    public class QueryBuilderTest
    {
        private readonly IQueryFormatter _queryFormatter;
        private readonly IQueryBuilder _queryBuilder;

        public QueryBuilderTest()
        {
            _queryFormatter = Substitute.For<IQueryFormatter>();
            _queryBuilder = new QueryBuilder(_queryFormatter);
        }

        [Fact]
        public void BuildTextTest()
        {
            // Arrange
            var text = "Ali is someone !";

            // Act
            _queryBuilder.BuildText(text);

            // Assert
            Assert.Equal(text, _queryBuilder.GetQuery().Text);
        }

        [Fact]
        public void BuildWordsBySign_Should_OrganizeWordsCorrectly_ForPlusAndMinusSigns()
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

            _queryBuilder.BuildText(text);

            // Act
            _queryBuilder.BuildWordsBySign(signs);
            var query = _queryBuilder.GetQuery();

            // Assert
            Assert.Equal(new List<string> { "AMIR" }, query.WordsBySign['+']);
            Assert.Equal(new List<string> { "REZA" }, query.WordsBySign['-']);
            
        }
    }
}
