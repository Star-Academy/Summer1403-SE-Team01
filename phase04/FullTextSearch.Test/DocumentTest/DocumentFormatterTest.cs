using FullTextSearch.Controller.DocumentController;
using Xunit;
using Assert = Xunit.Assert;


namespace FullTextSearch.Test;
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
    public void ToUpperTest(
        string expected, string text)
    {
        var result = _sut.ToUpper(text);
        Assert.Equal(expected, result);
    }
    
    [NUnit.Framework.Theory]
    [InlineData(new[] { "salAm!" }, "salAm!")]
    [InlineData(new[] { "A", "L", "I" }, "A L I")]
    [InlineData(new[] { "reza", "!", "moHammmad", "", "", "", "", "-", "?", "reza" }, "reza ! moHammmad     - ? reza")]
    public void SplitTest(string[] expectedArray, string text)
    {
        var expected = new List<string>(expectedArray);
        //var result = _sut.Split();
        //Assert.Equal(expected, "ddd");
    }
    
    [NUnit.Framework.Theory]
    [InlineData("sal A m !", "sal                                A m !")]
    [InlineData("A L I" , "A L I")]
    public void RemoveExtraSpaceTest(string expected, string text)
    {
        //var result = _sut.RemoveExtraSpace(text);
        Assert.Equal(expected, "sss");
    }
}