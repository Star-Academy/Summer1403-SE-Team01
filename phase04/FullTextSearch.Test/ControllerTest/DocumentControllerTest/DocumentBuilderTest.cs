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
    private readonly IDocumentBuilder _sut;

    public DocumentBuilderTest()
    {
        _documentFormatter = Substitute.For<IDocumentFormatter>();
        _sut = new DocumentBuilder(_documentFormatter);
    }

    [Test]
    public void BuildName_ShouldSetName() // it always sets the name, what to say in When part in naming
    {
        // Arrange
        var name = "Ali";
        var expected = name;
        
        // Act
        _sut.BuildName(name);
        var actual = _sut.GetDocument().Name;
        
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
        _sut.BuildPath(path);
        var actual = _sut.GetDocument().Path;
        
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
        _sut.BuildText(text);
        var actual = _sut.GetDocument().Text;
        
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
        _sut.GetDocument().Text = text;
        _sut.BuildWords();
        var actual = _sut.GetDocument().Words;
        
        // Assert
        Assert.True(expected.SequenceEqual(_sut.GetDocument().Words));
    }

}
