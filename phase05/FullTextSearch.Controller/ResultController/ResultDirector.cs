using FullTextSearch.Controller.ResultController.Abstraction;
using FullTextSearch.Controller.SearchController;
using FullTextSearch.Controller.SearchController.Abstraction;
using FullTextSearch.Core;

namespace FullTextSearch.Controller.ResultController;

public class ResultDirector : IResultDirector
{
    public void Construct(IResultBuilder resultBuilder, Core.Query query, Dictionary<string, IEnumerable<Document>> invertedIndex)
    {
        resultBuilder.BuildDocumentsBySign(new List<ISearcher>(){new MinusSearcher(), new PlusSearcher(), new NoSignedSearcher()}, query, invertedIndex);
        resultBuilder.BuildDocuments(new List<IFilter>(){new NoSignedFilter(), new PlusFilter(), new MinusFilter()},invertedIndex);
    }
}