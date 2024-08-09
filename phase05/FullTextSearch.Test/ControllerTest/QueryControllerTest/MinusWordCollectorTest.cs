using System.Text.RegularExpressions;
using FullTextSearch.Controller.QueryController;
using FullTextSearch.Controller.QueryController.Abstraction;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ControllerTest.QueryControllerTest;

public class MinusWordCollectorTest
{
    private readonly IWordCollector _sut;
    
    public MinusWordCollectorTest()
    {
        _sut = new MinusWordCollector();
    }
    
    [Fact]
    public void Collect_ShouldCollectMinusWords_WhenGivenTheQueryText()
    {
        // Arrange
        var text = "+ali -hassan \"karim zahra\" +\"ali mohammad\" kabir hoda -leila"; 
        var pattern = @"(-""[^""]+""|-\w+)";
        
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
        var minusCollectedWords = new List<string>() {
            "-hassan",
            "-\"amir ali\"", 
            "-zahra", 
            "-\"hadi\""
        };
        
        var expected = minusCollectedWords
            .Select(c => c.Trim('-').Trim('"').ToUpper());
        
        // Act
        var actual = _sut
            .RemovePrefix(minusCollectedWords);
        
        // Assert
        Assert.Equivalent(expected, actual);
    }
}