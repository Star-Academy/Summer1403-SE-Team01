using FullTextSearch.Controller.QueryController;
using FullTextSearch.Controller.QueryController.Abstraction;
using FullTextSearch.Core;
using NSubstitute;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ControllerTest.QueryControllerTest;

public class QueryBuilderTests
{
    private readonly IWordCollectorDriver _wordCollectorDriver;
    private readonly QueryBuilder _sut;

    public QueryBuilderTests()
    {
        _wordCollectorDriver = Substitute.For<IWordCollectorDriver>();
        _sut = new QueryBuilder(_wordCollectorDriver);
    }

    [Fact]
    public void BuildText_ShouldSetTextInQuery()
    {
        // Arrange
        var text = "sample text";
        var expected = text;

        // Act
        _sut.BuildText(text);
        var actual = _sut.GetQuery().Text;

        // Assert
        var query = _sut.GetQuery();
        Assert.Equivalent(expected, actual);
    }
    
    
    [Fact]
    public void BuildWordsBySign_ShouldPopulateWordsBySign()
    {
        // Arrange
        var text = "+ali -hassan \"karim zahra\" +\"ali mohammad\" kabir hoda -leila";
        var collectors = new List<IWordCollector>()
        {
            new MinusWordCollector(),
            new PlusWordsCollector(),
            new NoSignedWordCollector()
        };
        
        var query = new Query();
        query.Text = text;
        _wordCollectorDriver.DriveCollect(collectors, query);
        var expected = query.WordsBySign;

        // Act
        _sut.BuildWordsBySign(collectors);
        var actual = _sut.GetQuery().WordsBySign;

        // Assert
        Assert.Equivalent(expected, actual);
    }
}