using FullTextSearch.Controller.DocumentController;
using Xunit;
using Assert = Xunit.Assert;


namespace FullTextSearch.Test.ControllerTest.DocumentControllerTest;
public class DocumentFormatterTest
{
    private readonly DocumentFormatter _sut;

    public DocumentFormatterTest()
    {
        _sut = new DocumentFormatter();
    }
    
    [Xunit.Theory]
    [InlineData("HEY EVERYBODY! WHATSAPP", "Hey Everybody! Whatsapp")]
    [InlineData("ALI  REZA", "ALI  REZA")]
    [InlineData("ALI REZA", "ali reza")]
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
    public void Split_ShouldSplitTextBasedOnGivenRegex(string[] expectedArray, string text)
    {
        // Arrange
        var expected = new List<string>(expectedArray);
        
        // Act
        var actual = _sut.Split(text, " ");
        
        // Assert
        Assert.Equal(expected, actual);
    }
}