using FullTextSearch.Controller.QueryController;
using FullTextSearch.Controller.QueryController.Abstraction;
using FullTextSearch.Core;
using NSubstitute;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ControllerTest.QueryControllerTest;

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
    public void BuildText_ShouldSetText()
    {
        // Arrange
        var text = "Ali is someone !";
        var expected = text;
        
        // Act
        _sut.BuildText(text);
        var actual = _sut.GetQuery().Text;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact] public void BuildWordsBySign_ShouldFillWordsBySign()
    {
        // Arrange
        string text = "cat +reza -demand", uppered = "CAT +REZA -DEMAND";
        var signs = new[] { '+', '-' };
        var split = new List<string> { "CAT", "+REZA", "-DEMAND" };
        var plus = new List<string> { "+REZA" };
        var minus = new List<string> { "-DEMAND" };
        var unsigned = new List<string> { "CAT" };
        var removedPlus = new List<string> { "REZA" };
        var removedMinus = new List<string> { "DEMAND" };
        
        _queryFormatter.ToUpper(text).Returns(uppered);
        _queryFormatter.Split(uppered, " ").Returns(split);
        
        _queryFormatter.CollectBySign(split, '+').Returns(plus);
        _queryFormatter.CollectBySign(split, '-').Returns(minus);
        _queryFormatter.CollectBySign(split, ' ').Returns(unsigned);

        _queryFormatter.RemovePrefix(plus).Returns(removedPlus);
        _queryFormatter.RemovePrefix(minus).Returns(removedMinus);

        Query query = new Query();
        query.Text = text;
        query.WordsBySign = new Dictionary<char, IEnumerable<string>>()
        {
            {'+', removedPlus},
            {'-', removedMinus},
            {' ', unsigned}
        };
        
        // Act
        _sut.BuildText(text);
        _sut.BuildWordsBySign(signs);

        // Assert
        Assert.True(query.Equals(_sut.GetQuery()));
    }
}