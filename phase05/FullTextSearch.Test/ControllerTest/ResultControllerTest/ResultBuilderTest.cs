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
        var searchers = new List<ISearcher>() {new MinusSearcher(), new PlusSearcher(), new NoSignedSearcher()};
        
        var query = new Query();
        query.Text = "cat +reza -demand";
        query.WordsBySign = new Dictionary<char, IEnumerable<string>>()
        {
            {'+', new List<string>() {"reza"}},
            {'-', new List<string>() {"demand"}},
            {' ', new List<string>() {"cat"}}
        };
        var result = new Result();
        
        var documentList = DataSample.GetDocuments();

        var document1 = documentList[0];
        var document2 = documentList[1];
        var document3 = documentList[2];
        
        var invertedIndexMap = DataSample.GetInvertedIndexMap(document1, 
            document2, document3);
        
        _searcherDriver.DriveSearch(searchers, query, result, invertedIndexMap);
        var expected = result.DocumentsBySign;
        
        // Act
        _sut.BuildDocumentsBySign(searchers, query, invertedIndexMap);
        var actual = _sut.GetResult().DocumentsBySign;
        
        // Assert
        Assert.Equivalent(expected, actual);
    }
    
    [Fact]
    public void BuildDocuments_ShouldFillResultDocuments_WhenGivenFiltersAndInvertedIndex()
    {
        // Arrange
        var filters = new List<IFilter>() {new NoSignedFilter(), new MinusFilter(), new PlusFilter()};
        var result = new Result();
        
        var documentList = DataSample.GetDocuments();

        var document1 = documentList[0];
        var document2 = documentList[1];
        var document3 = documentList[2];

        var invertedIndexMap = DataSample.GetInvertedIndexMap(document1, 
            document2, document3);
        
        result.Documents = new UniversalSearch().GetUniversal(invertedIndexMap);
        _filterDriver.DriveFilterer(filters, result);
        var expected = result.Documents;

        // Act
        _sut.BuildDocuments(filters, invertedIndexMap);
        var actual = _sut.GetResult().Documents;
        
        // Assert
        Assert.Equivalent(expected, actual);
    }
}
