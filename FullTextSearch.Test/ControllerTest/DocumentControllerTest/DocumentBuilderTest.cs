using System.Text.RegularExpressions;
using FullTextSearch.Controller.DocumentController;
using FullTextSearch.Controller.DocumentController.Abstraction;
using InvertedIndex.Controller.Document;
using NSubstitute;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ControllerTest.DocumentControllerTest;

public class DocumentBuilderTest
{
    private readonly IDocumentFormatter _documentFormatter;
    private readonly IDocumentBuilder _documentBuilder;

    public DocumentBuilderTest()
    {
        _documentFormatter = Substitute.For<IDocumentFormatter>();
        _documentBuilder = new DocumentBuilder(_documentFormatter);
    }

    [Test]
    public void BuildName_ShouldSetName() // it always sets the name, what to say in When part in naming
    {
        // Arrange
        var name = "Ali";
        var expected = name;
        
        // Act
        _documentBuilder.BuildName(name);
        var actual = _documentBuilder.GetDocument().Name;
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Test]
    public void BuildPath_ShouldSetPath()
    {
        // Arrange
        var path = "Ali";
        var expected = path;
        
        // Act
        _documentBuilder.BuildPath(path);
        var actual = _documentBuilder.GetDocument().Path;
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Test]
    public void BuildText_ShouldSetText()
    {
        // Arrange
        var text = "Ali is someone !";
        var expected = text;
        
        // Act
        _documentBuilder.BuildText(text);
        var actual = _documentBuilder.GetDocument().Text;
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Test]
    public void BuildWords_ShouldFillWords()
    {
        // Arrange
        var text = "Ali is someone !";
        var upper = text.ToUpper();
        var expected = Regex.Split(text, " ");  // remove whitespaces if split based on " "
        
        _documentFormatter.ToUpper(text).Returns(upper);
        _documentFormatter.Split(upper, " ").Returns(expected);

        // Act
        _documentBuilder.GetDocument().Text = text;
        _documentBuilder.BuildWords();
        var actual = _documentBuilder.GetDocument().Words;
        
        // Assert
        Assert.True(expected.SequenceEqual(_documentBuilder.GetDocument().Words));
    }

}
