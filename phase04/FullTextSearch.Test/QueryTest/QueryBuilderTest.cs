using FullTextSearch.Controller.QueryController;
using FullTextSearch.Controller.QueryController.Abstraction;
using FullTextSearch.Core;
using FullTextSearch.Service.InitializeService;
using NSubstitute;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.QueryTest;

public class QueryBuilderTest
{
    private readonly IQueryFormatter _queryFormatter;
    private readonly IQueryBuilder _queryBuilder;

    public QueryBuilderTest()
    {
        _queryFormatter = Substitute.For<IQueryFormatter>();
        _queryBuilder = new QueryBuilder(_queryFormatter);
    }

    [Test]
    public void BuildTextTest()
    {
        // Arange
        var text = "Ali is someone !";
        
        // Act
        _queryBuilder.BuildText(text);
        
        // Assert
        Xunit.Assert.Equal(text, _queryBuilder.GetQuery().Text);
    }
    
    [Fact]
    public void BuildWordsBySign_ShouldGroupWordsBySign()
    {
        // Arrange

        string text = "cat +mohammad";
        var signs = new[] { '+', '-' };

        // Setting up the mock behavior
        _queryFormatter.ToUpper(text).Returns(text.ToUpper());
        var splittedText = new List<string> { "CAT", "+MOHAMMAD" };
        _queryFormatter.Split(text.ToUpper(), " ").Returns(splittedText);
        _queryFormatter.CollectBySign(splittedText, '+').Returns(new List<string> { "+MOHAMMAD" });
        _queryFormatter.CollectBySign(splittedText, '-').Returns(new List<string> {  });
        _queryFormatter.RemovePrefix(new List<string> { "+MOHAMMAD" }).Returns(new List<string> { "MOHAMMAD" });

        // Act
        _queryBuilder.BuildText(text); // Initialize the text
        _queryBuilder.BuildWordsBySign(signs);
        var query = _queryBuilder.GetQuery();

        // Assert
        Assert.NotNull(query.WordsBySign);
        Assert.True(query.WordsBySign.ContainsKey('+'));
        Assert.True(query.WordsBySign.ContainsKey('-'));
        Assert.True(query.WordsBySign.ContainsKey(' '));
        Assert.Equal(new List<string> { "WORD2" }, query.WordsBySign['+']);
        Assert.Equal(new List<string> { "WORD3" }, query.WordsBySign['-']);
        Assert.Equal(new List<string> { "WORD1", "WORD4" }, query.WordsBySign[' ']);
    }
}