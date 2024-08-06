using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using NSubstitute;
using FullTextSearch.Controller.QueryController;
using FullTextSearch.Controller.QueryController.Abstraction;
using FullTextSearch.Controller.TextFormatter.Abstraction;
using FullTextSearch.Core;
using Assert = Xunit.Assert;

public class QueryBuilderTests
{
    private readonly IQueryFormatter _queryFormatter;
    private readonly ITextFormatter _textFormatter;
    private readonly QueryBuilder _queryBuilder;

    public QueryBuilderTests()
    {
        _queryFormatter = Substitute.For<IQueryFormatter>();
        _textFormatter = Substitute.For<ITextFormatter>();
        _queryBuilder = new QueryBuilder(_queryFormatter, _textFormatter);
    }

    [Fact]
    public void BuildText_ShouldSetTextInQuery()
    {
        // Arrange
        var text = "sample text";

        // Act
        _queryBuilder.BuildText(text);

        // Assert
        var query = _queryBuilder.GetQuery();
        Assert.Equal(text, query.Text);
    }

    [Fact]
    public void BuildWordsBySign_ShouldPopulateWordsBySign()
    {
        // Arrange
        var text = "+cat! -z \"ali is\"";
        var signs = new[] { '+', '-' };
        var splittedText = new List<string> { "+CAT!" , "-Z", "\'ali", "is\""};
        var quoteIndices = new List<int>(){2, 3}; 
        var indicesToRemove = new List<int>(){2, 3};
        var filteredWords = new List<string>(){ "+CAT!", "-Z" };
        var concatenatedQuotes = new List<string>(){"\"ali is\""};

        _queryFormatter.Split(Arg.Any<string>(), " ").Returns(splittedText);
        _queryFormatter.ToUpper(Arg.Any<string>()).Returns(callInfo => callInfo.Arg<string>().ToUpper());
        _textFormatter.GetQuoteIndices(Arg.Any<List<string>>()).Returns(quoteIndices);
        _textFormatter.GetIndicesToRemove(Arg.Any<List<int>>()).Returns(indicesToRemove);
        _textFormatter.FilterOutIndices(Arg.Any<List<string>>(), Arg.Any<List<int>>()).Returns(filteredWords);
        _textFormatter.ConcatenateQuotedWords(Arg.Any<List<string>>(), Arg.Any<List<int>>()).Returns(concatenatedQuotes);
        
        _queryFormatter.CollectBySign(
            Arg.Is<IEnumerable<string>>(x => x.SequenceEqual(new List<string> { "+CAT!" , "-Z" })), 
            '+').Returns(new List<string> { "+CAT!" });
        
        _queryFormatter.CollectBySign(
            Arg.Is<IEnumerable<string>>(x => x.SequenceEqual(new List<string> { "-Z" })), 
            '-').Returns(new List<string> { "-Z" });

        _queryFormatter.RemovePrefix(
                Arg.Is<IEnumerable<string>>(x =>
                    x.SequenceEqual(new List<string> { "+CAT!" })))
            .Returns(new List<string>() { "CAT!" });
        
        _queryFormatter.RemovePrefix(
                Arg.Is<IEnumerable<string>>(x => 
                    x.SequenceEqual(new List<string> { "-Z" })))
            .Returns(new List<string>(){"Z"});
        
        _queryFormatter.RemovePrefix(
                Arg.Is<IEnumerable<string>>(x => 
                    x.SequenceEqual(new List<string> { "\"ali is\"" })))
            .Returns(new List<string>(){"ali is"});
        
        
        _queryFormatter.CollectBySign(
            Arg.Is<IEnumerable<string>>(x => x.SequenceEqual(new List<string> { "\"ali is\"" })), 
            '+').Returns(new List<string> {  });
        
        _queryFormatter.CollectBySign(
            Arg.Is<IEnumerable<string>>(x => x.SequenceEqual(new List<string> { "\"ali is\"" })), 
            '-').Returns(new List<string> {  });
        
        _queryBuilder.BuildText(text);

        // Act
        _queryBuilder.BuildWordsBySign(signs);

        // Assert
        var query = _queryBuilder.GetQuery();

        // Expected dictionary
        var expected = new Dictionary<char, List<string>>
        {
            { '+', new List<string> { "CAT!" } },
            { '-', new List<string>() {"Z"} },
            { ' ', new List<string>() {"ali is"}}
        };

        // Compare keys
        Assert.Equal(expected.Keys, query.WordsBySign.Keys);

        // Compare values for each key
        foreach (var key in expected.Keys)
        {
            Assert.Equal(expected[key], query.WordsBySign[key]);
        }
    }
}
