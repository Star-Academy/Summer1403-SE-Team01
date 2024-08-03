using FullTextSearch.Controller.DocumentController;
using FullTextSearch.Controller.DocumentController.Abstraction;
using FullTextSearch.Controller.InvertedIndexController;
using FullTextSearch.Controller.Read.Abstraction;
using FullTextSearch.Core;
using FullTextSearch.Service.InitializeService;
using InvertedIndex.Abstraction.Read;
using NSubstitute;

namespace FullTextSearch.Test.ServiceTest.InitializeServiceTest;

public class InitializeServiceTest
{
    private readonly IFileReader _fileReader;
    private readonly IDocumentDirector _documentDirector;
    private readonly IInvertedIndexMapper _invertedIndexMapper;
    private readonly IDirectory _directory;
    private readonly IPath _path;
    private readonly InitializeService _sut;

    public InitializeServiceTest()
    {
        _fileReader = Substitute.For<IFileReader>();
        _documentDirector = Substitute.For<IDocumentDirector>();
        _invertedIndexMapper = Substitute.For<IInvertedIndexMapper>();
        _directory = Substitute.For<IDirectory>();
        _path = Substitute.For<IPath>();
        _sut = new InitializeService(_fileReader, _documentDirector, _invertedIndexMapper);
    }

    [Test]
    public async Task InitializeTest()
    {
        // Arrange
        var directoryPath = "./";
        List<Document> documents = new List<Document>();
        var paths = new List<string?>() {"./Resources/EnglishData/57110", "./Resources/EnglishData/58043", "./Resources/EnglishData/558044"};
        var names = new List<string?>() {"57110", "58043", "558044"};
        var texts = new List<string?>() {"", "", ""};
        DocumentFormatter documentFormatter = new DocumentFormatter();
        var documentBuilders = new List<DocumentBuilder>() {new DocumentBuilder(documentFormatter), new DocumentBuilder(documentFormatter), new DocumentBuilder(documentFormatter)};
        

        _directory.getFiles(directoryPath).Returns(paths);

        _path.GetName(paths[0]).Returns(names[0]);
        _path.GetName(paths[1]).Returns(names[1]);
        _path.GetName(paths[2]).Returns(names[2]);

        _fileReader.ReadAsync(paths[0]).Returns(texts[0]);
        _fileReader.ReadAsync(paths[1]).Returns(texts[1]);
        _fileReader.ReadAsync(paths[2]).Returns(texts[2]);

        _documentDirector.Construct(names[0], paths[0], texts[0], documentBuilders[0]);
        _documentDirector.Construct(names[0], paths[0], texts[0], documentBuilders[1]);
        _documentDirector.Construct(names[0], paths[0], texts[0], documentBuilders[2]);
        
        documents.Add(documentBuilders[0].GetDocument());
        documents.Add(documentBuilders[1].GetDocument());
        documents.Add(documentBuilders[2].GetDocument());

        Document document1 = new Document();
        document1.Name = "Doc1";
        document1.Path = "./ResourcesTest/Doc1";
        document1.Text = "reza ali mohammad hello";
        document1.Words = new List<string> {"cat", "reza"};
            
            
        Document document2 = new Document();
        document2.Name = "Doc2";
        document2.Path = "./ResourcesTest/Doc2";
        document2.Text = "reza ali mohammad hello";
        document2.Words = new List<string> {"cat", "reza", "demand"};
            
            
        Document document3 = new Document();
        document3.Name = "Doc3";
        document3.Path = "./ResourcesTest/Doc3";
        document3.Text = "reza ali mohammad hello";
        document3.Words = new List<string> {"cat", "ali"};

        var expected = new Dictionary<string, IEnumerable<Document>>
        {
            { "cat", new List<Document> { document1, document2, document3} },
            { "reza", new List<Document> {document1, document2} },
            { "demand", new List<Document> { document2 } }
        };

        _invertedIndexMapper.Map(documents).Returns(expected);
        
        // Act
        var result = await _sut.Initialize(directoryPath);
        
        // Assert
        Xunit.Assert.Equal(result.Keys.Count, expected.Keys.Count);
        foreach (var entry in expected)
        {
            Xunit.Assert.True(entry.Value.SequenceEqual(result[entry.Key]));
        }    
    }
}