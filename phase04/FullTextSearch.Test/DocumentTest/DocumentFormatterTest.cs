using FullTextSearch.Controller.DocumentController;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test
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
        public void ToUpper_ShouldConvertToUpperCase(string expected, string text)
        {
            // Act
            var result = _sut.ToUpper(text);

            // Assert
            Assert.Equal(expected, result);
        }
        
        [Xunit.Theory]
        [InlineData("This is a test.", " ", new[] { "This", "is", "a", "test." })]
        [InlineData("amir!", " ", new[] { "amir!" })]
        public void Split_ShouldReturnExpectedResults(string queryText, string regex, string[] expected)
        {
            // Act
            IEnumerable<string> result = _sut.Split(queryText, regex);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}