using FullTextSearch.Controller.QueryController;
using Xunit;

namespace FullTextSearch.Test.ControllerTest.QueryControllerTest;
using Assert = Xunit.Assert;

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
    public void ToUpper_ShouldUpperText(string expected, string text)
    {
        // Arranged
        
        // Act
        var actual = _sut.ToUpper(text);
        
        // Assert
        Assert.Equal(expected, actual);
    }

    [Xunit.Theory]
     [InlineData(new[] { "salAm!" }, "salAm!")]
     [InlineData(new[] { "A", "L", "I" }, "A L I")]
     [InlineData(new[] { "reza", "!", "moHammmad", "", "", "", "", "-", "?", "reza" }, "reza ! moHammmad     - ? reza")]
    public void Split_ShouldSplitTxt(string[] expectedArray, string text)
    {
        // Arranged
        var expected = new List<string>(expectedArray);

        // Act
        var actual = _sut.Split(text, " ");
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Xunit.Theory]
    [InlineData(new[] {"+ali", "+mohammad"}, new[] {"+ali", "-reza", "zahra", "+mohammad"}, '+')]
    [InlineData(new[] {"-reza"}, new[] {"+ali", "-reza", "zahra", "+mohammad"}, '-')]
    [InlineData(null, new[] {"+ali", "-reza", "zahra", "+mohammad"}, '*')]
    public void ExtractBySign_ShouldCollectWordsBasedOnSign(string[]? expectedArray, string[] splitListArray, char c)
    {
        // Arrange
        var splitList = new List<string>(splitListArray);
        var expected = expectedArray == null ? new List<string>() : new List<string>(expectedArray);
        
        // Act
        var actual = _sut.CollectBySign(splitList, c);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Xunit.Theory]
    [InlineData(new[] {"ali", "reza", "zahra", "mohammad"}, new[] {"+ali", "+reza", "+zahra", "+mohammad"})]   
    [InlineData(new[] {"ali", "reza", "zahra", "mohammad"}, new[] {"-ali", "-reza", "-zahra", "-mohammad"})]
    public void SeparatePrefix_ShouldRemovePrefixOfWords(string[] expectedArray, string[] CollectedArray)
    {
        // Arrange
        var collectedList = new List<string>(CollectedArray);
        var expected = new List<string>(expectedArray);

        // Act
        var actual = _sut.RemovePrefix(collectedList);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}