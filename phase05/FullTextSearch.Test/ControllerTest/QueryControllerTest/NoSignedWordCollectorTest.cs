using System.Text.RegularExpressions;
using FullTextSearch.Controller.QueryController;
using FullTextSearch.Controller.QueryController.Abstraction;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ControllerTest.QueryControllerTest;

public class NoSignedWordCollectorTest
{
    private readonly IWordCollector _sut;
    
    public NoSignedWordCollectorTest()
    {
        _sut = new NoSignedWordCollector();
    }
    
    [Fact]
    public void Collect_ShouldCollectMinusWords_WhenGivenTheQueryText()
    {
        // Arrange
        var text = "+ali -hassan \"karim zahra\" +\"ali mohammad\" kabir hoda -leila"; 
        var pattern = @"(\+""[^""]+""|\+\w+)|(-""[^""]+""|-\w+)|(""[^""]+""|\b\w+\b)";
        
        var regex = new Regex(pattern);
        var matches = regex.Matches(text);
        var expected = matches.Select(m => m.Value);
        
        // Act
        var actual = _sut.Collect(text);
        
        // Assert
        Assert.Equivalent(expected, actual);
    }
    
    [Fact]
    public void RemovePrefix_ShouldRemovePrefixOfMinusWords_WhenGivenTheListOfMinusWords()
    {
        // Arrange
        var noSignedCollectedWords = new List<string>()
        {
            "hassan", 
            "\"this is a name\"", 
            "\"zahra is someone\"", 
            "another"
        };
        var expected = noSignedCollectedWords
            .Where(c => !c.StartsWith("+") && !c.StartsWith("-"))
            .Select(c => c.Trim('"').ToUpper());
        
        // Act
        var actual = _sut
            .RemovePrefix(noSignedCollectedWords);

        // Assert
        Assert.Equivalent(expected, actual);
    }
}