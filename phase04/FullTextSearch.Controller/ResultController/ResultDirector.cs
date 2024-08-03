using FullTextSearch.Controller.ResultController.Abstraction;
using FullTextSearch.Controller.SearchController;
using FullTextSearch.Core;
using Query = NSubstitute.Core.Query;

namespace FullTextSearch.Controller.ResultController;

public class ResultDirector : IResultDirector
{
    public void Construct(IResultBuilder resultBuilder, Core.Query query, Dictionary<string, IEnumerable<Document>> invertedIndex)
    {
        //var searchers = new List<ISearcher>(){new MinusSearcher(), new PlusSearcher(), new NoSignedSearcher()};
       // var filterers = new List<IFilter>(){new NoSignedFilter(), new PlusFilter(), new MinusFilter()};

        resultBuilder.BuildDocumentsBySign(new List<ISearcher>(){new MinusSearcher(), new PlusSearcher(), new NoSignedSearcher()}, query, invertedIndex);
        resultBuilder.BuildDocuments(new List<IFilter>(){new NoSignedFilter(), new PlusFilter(), new MinusFilter()},invertedIndex);
    }
}