public class SearcherDriver {
    public void DriveSearch(IEnumerable<ISearcher> searchers, Query query, Result result, Dictionary<string,List<Document>> dictionary)
    {
        searchers.ToList().ForEach(s=>result.signToDocumentListDictionary.Add(s.Sign, s.Search(query, dictionary)));
    }
}