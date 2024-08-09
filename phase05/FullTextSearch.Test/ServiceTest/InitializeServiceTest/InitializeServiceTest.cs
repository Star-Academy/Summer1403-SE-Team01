using FullTextSearch.Controller.DocumentController;
using FullTextSearch.Controller.DocumentController.Abstraction;
using FullTextSearch.Controller.InvertedIndexController;
using FullTextSearch.Controller.InvertedIndexController.Abstraction;
using FullTextSearch.Controller.Read.Abstraction;
using FullTextSearch.Core;
using FullTextSearch.Service.InitializeService;
using FullTextSearch.Test.Data;
using NSubstitute;
using Xunit;
using Assert = Xunit.Assert;

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
        _sut = new InitializeService(_fileReader, _documentDirector, _invertedIndexMapper, _directory, _path);
    }

    [Fact]
    public async Task Initialize_ShouldInitializeDocumentsFromFilesInGivenDirectory_WhenDirectoryExists()
    {
        // Arrange
        var directoryPath = "./Resources/EnglishData";
        var paths = new List<string> { "./Resources/EnglishData/57110", "./Resources/EnglishData/58043", "./Resources/EnglishData/558044" };
        var names = new List<string> { "57110", "58043", "558044" };
        var texts = new List<string> { "", "", "" };
        var documentFormatter = new DocumentFormatter();
        var documentBuilders = new List<DocumentBuilder>();

        _directory.GetFiles(directoryPath).Returns(paths);

        for (int i = 0; i < paths.Count; i++)
        {
            _path.GetFileName(paths[i]).Returns(names[i]);
            _fileReader.ReadAsync(paths[i]).Returns(texts[i]);
            var documentBuilder = new DocumentBuilder(documentFormatter);
            _documentDirector.Construct(names[i], paths[i], texts[i], documentBuilder);
            documentBuilders.Add(documentBuilder);
        }

        var documents = documentBuilders.Select(builder => builder.GetDocument()).ToList();
        var documentList = DataSample.GetDocuments();
        var expected = DataSample.GetInvertedIndexMap(documentList[0], documentList[1], documentList[2]);

        _invertedIndexMapper.Map(documents).Returns(expected);

        // Act
        var result = await _sut.Initialize(directoryPath);

        // Assert
        Assert.Equal(result.Keys.Count, expected.Keys.Count);
        foreach (var entry in expected)
        {
            Assert.True(entry.Value.SequenceEqual(result[entry.Key]));
        }
    }
}
