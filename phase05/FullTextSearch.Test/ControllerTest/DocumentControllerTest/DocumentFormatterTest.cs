using FullTextSearch.Controller.DocumentController;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ControllerTest.DocumentControllerTest
{
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
        public void ToUpper_ShouldConvertTextToUpperCase_WhenGivenText(string expected, string text)
        {
            // Act
            var actual = _sut.ToUpper(text);

            // Assert
            Assert.Equivalent(expected, actual);
        }
        
        [Xunit.Theory]
        [InlineData("This is a test.", " ", new[] { "This", "is", "a", "test." })]
        [InlineData("amir!", " ", new[] { "amir!" })]
        public void Split_ShouldReturnExpectedWords_WhenGivenTextAndDelimiter(string queryText, string regex, string[] expected)
        {
            // Act
            var actual = _sut.Split(queryText, regex);

            // Assert
            Assert.Equivalent(expected, actual);
        }
    }
}