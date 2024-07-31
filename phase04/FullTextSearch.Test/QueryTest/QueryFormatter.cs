using FullTextSearch.Controller.QueryController;
using Xunit;

namespace InvertedIndex.Test.Query;

public class QueryFormatterTest
{
    private readonly QueryFormatter _sut;

    public QueryFormatterTest()
    {
        _sut = new QueryFormatter();
    }
    
    [NUnit.Framework.Theory]
    [InlineData("SALAM!", "salAm!")]
    [InlineData("ALI", "ALI")]
    [InlineData("REZA", "reza")]
    public void ToUpperTest(
        string expected, string text)
    {
        var result = _sut.ToUpper(text);
        Xunit.Assert.Equal(expected, result);
    }

     [NUnit.Framework.Theory]
     [InlineData(new[] { "salAm!" }, "salAm!")]
     [InlineData(new[] { "A", "L", "I" }, "A L I")]
     [InlineData(new[] { "reza", "!", "moHammmad", "", "", "", "", "-", "?", "reza" }, "reza ! moHammmad     - ? reza")]
    public void SplitTest(
        string[] expectedArray, string text)
    {
        var expected = new List<string>(expectedArray);
        //var result = _sut.Split();
        //Xunit.Assert.Equal(expected, "sss");
    }

    
    [NUnit.Framework.Theory]
    [InlineData("sal A m !", "sal                                A m !")]
    [InlineData("A L I" , "A L I")]
    public void RemoveExtraSpaceTest(string expected, string text)
    {
        //var result = _sut.RemoveExtraSpace(text);
        Xunit.Assert.Equal(expected, "Sss");
    }
    
    [NUnit.Framework.Theory]
    [InlineData(new[] {"+ali", "+mohammad"}, new[] {"+ali", "-reza", "zahra", "+mohammad"}, '+')]
    [InlineData(new[] {"-reza"}, new[] {"+ali", "-reza", "zahra", "+mohammad"}, '-')]
    [InlineData(null, new[] {"+ali", "-reza", "zahra", "+mohammad"}, '*')]
    public void ExtractBySignTest(string[] expectedArray, string[] listArray, char c)
    {
        var list = new List<string>(listArray);
        var expected = expectedArray == null ? new List<string>() : new List<string>(expectedArray);
        
        //var result = _sut.ExtractBySign(list , c);
        
        //Xunit.Assert.Equal(expected, "Sss");
    }
    
    [NUnit.Framework.Theory]
    [InlineData(new[] {"ali", "reza", "zahra", "mohammad"}, new[] {"+ali", "+reza", "+zahra", "+mohammad"})]   
    [InlineData(new[] {"ali", "-reza", "zahra", "mohammad"}, new[] {"+ali", "-reza", "zahra", "+mohammad"})] // exception hadelling if necessary
    public void SeparatePrefixTest(string[] expectedArray, string[] listArray)
    {
        var list = new List<string>(listArray);
        var expected = new List<string>(expectedArray);
        
        //var result = _sut.SeparatePrefix(list);
        
        //Assert.Equals(expected, result);
    }
}