using FullTextSearch.Controller.ResultController;
using FullTextSearch.Controller.ResultController.Abstraction;
using FullTextSearch.Controller.SearchController;
using FullTextSearch.Controller.SearchController.Abstraction;
using FullTextSearch.Core;
using FullTextSearch.Test.Data;
using NSubstitute;
using Xunit;
using Assert = Xunit.Assert;

namespace FullTextSearch.Test.ControllerTest.ResultControllerTest;

public class ResultBuilderTest
{
    private readonly ISearcherDriver _searcherDriver;
    private readonly IFilterDriver _filterDriver;
    private readonly ResultBuilder _sut;

    public ResultBuilderTest()
    {
        _searcherDriver = Substitute.For<ISearcherDriver>();
        _filterDriver = Substitute.For<IFilterDriver>();
        _sut = new ResultBuilder(_filterDriver, _searcherDriver);
    }

    [Fact]
    public void BuildDocumentsBySign_ShouldFillDocumentsBySign_WhenGivenSearchersAndQuery()
    {
        // Arrange
        IEnumerable<ISearcher> searchers = new List<ISearcher>() {new MinusSearcher(), new PlusSearcher(), new NoSignedSearcher()};
        
        Query query = new Query();
        query.Text = "cat +reza -demand";
        query.WordsBySign = new Dictionary<char, IEnumerable<string>>()
        {
            {'+', new List<string>() {"reza"}},
            {'-', new List<string>() {"demand"}},
            {' ', new List<string>() {"cat"}}
        };
        Result result = new Result();
        
        var documentList = DataSample.GetDocuments();

        Document document1 = documentList[0];
        Document document2 = documentList[1];
        Document document3 = documentList[2];
        
        var invertedIndexMap = DataSample.GetInvertedIndexMap(document1, 
            document2, document3);
        
        _searcherDriver.DriveSearch(searchers, query, result, invertedIndexMap);
        
        // Act
        _sut.BuildDocumentsBySign(searchers, query, invertedIndexMap);
        
        // Assert
        Assert.Equal(result.documentsBySign.Keys.Count, _sut.GetResult().documentsBySign.Keys.Count);
        foreach (var entry in _sut.GetResult().documentsBySign)
        {
            Assert.True(entry.Value.SequenceEqual(result.documentsBySign[entry.Key]));
        }
    }
    
    [Test]
    public void BuildDocuments_ShouldFillResultDocuments_WhenGivenFiltersAndInvertedIndex()
    {
        // Arrange
        IEnumerable<IFilter> filters = new List<IFilter>() {new NoSignedFilter(), new MinusFilter(), new PlusFilter()};
        Result result = new Result();
        
        var documentList = DataSample.GetDocuments();

        Document document1 = documentList[0];
        Document document2 = documentList[1];
        Document document3 = documentList[2];

        var invertedIndexMap = DataSample.GetInvertedIndexMap(document1, 
            document2, document3);
        
        result.documents = new UniversalSearch().GetUniversal(invertedIndexMap);
        _filterDriver.DriveFilterer(filters, result);      

        // Act
        _sut.BuildDocuments(filters, invertedIndexMap);
        
        // Assert
        Assert.True(result.documents.SequenceEqual(_sut.GetResult().documents));
    }
}
