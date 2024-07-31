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
        private readonly IDocumentBuilder _documentBuilder;

        public DocumentBuilderTest()
        {
            _documentFormatter = Substitute.For<IDocumentFormatter>();
            _documentBuilder = new DocumentBuilder(_documentFormatter);
        }

        [Fact]
        public void BuildName_ShouldSetDocumentName()
        {
            // Arrange
            var name = "Ali";
            
            // Act
            _documentBuilder.BuildName(name);
            
            // Assert
            Assert.Equal(name, _documentBuilder.GetDocument().Name);
        }
        
        [Fact]
        public void BuildPath_ShouldSetDocumentPath()
        {
            // Arrange
            var path = "/document";
            
            // Act
            _documentBuilder.BuildPath(path);
            
            // Assert
            Assert.Equal(path, _documentBuilder.GetDocument().Path);
        }
        
        [Fact]
        public void BuildText_ShouldSetDocumentText()
        {
            // Arrange
            var text = "Ali is someone!";
            
            // Act
            _documentBuilder.BuildText(text);
            
            // Assert
            Assert.Equal(text, _documentBuilder.GetDocument().Text);
        }
        
        [Fact]
        public void BuildWords_ShouldSetDocumentWords()
        {
            // Arrange
            var sampleText = "hello world";
            var upperText = sampleText.ToUpper();
            var expectedWords = new List<string> { "HELLO", "WORLD" };

            _documentFormatter.ToUpper(sampleText).Returns(upperText);
            _documentFormatter.Split(upperText, " ").Returns(expectedWords);

            _documentBuilder.BuildText(sampleText);

            // Act
            _documentBuilder.BuildWords();
            var document = _documentBuilder.GetDocument();

            // Assert
            Assert.Equal(expectedWords, document.Words);
        }
    }
}
