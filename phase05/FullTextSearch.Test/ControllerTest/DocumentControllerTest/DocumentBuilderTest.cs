using FullTextSearch.Controller.DocumentController;
using FullTextSearch.Controller.DocumentController.Abstraction;
using NSubstitute;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ControllerTest.DocumentControllerTest
{
    public class DocumentBuilderTest
    {
        private readonly IDocumentFormatter _documentFormatter;
        private readonly IDocumentBuilder _sut;

        public DocumentBuilderTest()
        {
            _documentFormatter = Substitute.For<IDocumentFormatter>();
            _sut = new DocumentBuilder(_documentFormatter);
        }

        [Fact]
        public void BuildName_ShouldSetDocumentName_WhenGivenName()
        {
            // Arrange
            var expected = "Ali";
            
            // Act
            _sut.BuildName(expected);
            var actual = _sut.GetDocument().Name;
            
            // Assert
            Assert.Equivalent(expected, actual);
        }
        
        [Fact]
        public void BuildPath_ShouldSetDocumentPath_WhenGivenPath()
        {
            // Arrange
            var expected = "/document";
            
            // Act
            _sut.BuildPath(expected);
            var actual = _sut.GetDocument().Path;
            
            // Assert
            Assert.Equivalent(expected, actual);
        }
        
        [Fact]
        public void BuildText_ShouldSetDocumentText_WhenGivenText()
        {
            // Arrange
            var expected = "Ali is someone!";
            
            // Act
            _sut.BuildText(expected);
            var actual = _sut.GetDocument().Text;
            
            // Assert
            Assert.Equivalent(expected, actual);
        }
        
        [Fact]
        public void BuildWords_ShouldSetDocumentWords_WhenTextIsBuilt()
        {
            // Arrange
            var sampleText = "hello world";
            var upperText = sampleText.ToUpper();
            var expected = new List<string> { "HELLO", "WORLD" };

            _documentFormatter.ToUpper(sampleText).Returns(upperText);
            _documentFormatter.Split(upperText, " ").Returns(expected);

            _sut.BuildText(sampleText);

            // Act
            _sut.BuildWords();
            var actual = _sut.GetDocument().Words;

            // Assert
            Assert.Equivalent(expected, actual);
        }
    }
}