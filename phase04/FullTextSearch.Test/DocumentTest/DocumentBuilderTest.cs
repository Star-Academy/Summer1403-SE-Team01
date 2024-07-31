using FullTextSearch.Controller.DocumentController;
using FullTextSearch.Controller.DocumentController.Abstraction;
using FullTextSearch.Core;
using InvertedIndex.Controller.Document;
using NSubstitute;
using Xunit;

namespace FullTextSearch.Test.DocumentTest;

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
    public void BuildNameTest()
    {
        // Arange
        var name = "Ali";
        
        // Act
        _documentBuilder.BuildName(name);
        
        // Assert
        Xunit.Assert.Equal(name, _documentBuilder.GetDocument().Name);

    }
    
    [Test]
    public void BuildPath()
    {
        // Arange
        var path = "Ali";
        
        // Act
        _documentBuilder.BuildPath(path);
        
        // Assert
        Xunit.Assert.Equal(path, _documentBuilder.GetDocument().Path);
    }
    
    [Test]
    public void BuildTextTest()
    {
        // Arange
        var text = "Ali is someone !";
        
        // Act
        _documentBuilder.BuildText(text);
        
        // Assert
        Xunit.Assert.Equal(text, _documentBuilder.GetDocument().Text);
    }
    
    [Test]
    public void BuildWordsTest()
    {
        // Arange
        var text = "Ali is someone !";
        Document document = new Document();
        document.Text = text;
        document.Words = _documentFormatter.Split(_documentFormatter.ToUpper(document.Text), " ");
        
        // Act
        _documentBuilder.GetDocument().Text = text;
        _documentBuilder.BuildWords();
        
        // Assert
        Xunit.Assert.True(document.Words.SequenceEqual(_documentBuilder.GetDocument().Words));
    }

}
