using FullTextSearch.Controller.DocumentController;
using FullTextSearch.Controller.DocumentController.Abstraction;
using FullTextSearch.Core;
using InvertedIndex.Controller.Document;
using NSubstitute;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.DocumentTest
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
            var name = "Ali";
            
            // Act
            _sut.BuildName(name);
            
            // Assert
            Assert.Equal(name, _sut.GetDocument().Name);
        }
        
        [Fact]
        public void BuildPath_ShouldSetDocumentPath_WhenGivenPath()
        {
            // Arrange
            var path = "/document";
            
            // Act
            _sut.BuildPath(path);
            
            // Assert
            Assert.Equal(path, _sut.GetDocument().Path);
        }
        
        [Fact]
        public void BuildText_ShouldSetDocumentText_WhenGivenText()
        {
            // Arrange
            var text = "Ali is someone!";
            
            // Act
            _sut.BuildText(text);
            
            // Assert
            Assert.Equal(text, _sut.GetDocument().Text);
        }
        
        [Fact]
        public void BuildWords_ShouldSetDocumentWords_WhenTextIsBuilt()
        {
            // Arrange
            var sampleText = "hello world";
            var upperText = sampleText.ToUpper();
            var expectedWords = new List<string> { "HELLO", "WORLD" };

            _documentFormatter.ToUpper(sampleText).Returns(upperText);
            _documentFormatter.Split(upperText, " ").Returns(expectedWords);

            _sut.BuildText(sampleText);

            // Act
            _sut.BuildWords();
            var document = _sut.GetDocument();

            // Assert
            Assert.Equal(expectedWords, document.Words);
        }
    }
}