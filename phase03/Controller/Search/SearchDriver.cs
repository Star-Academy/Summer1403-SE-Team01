public class SearcherDriver {
    public SearcherDriver(IEnumerable<ISearcher> signables, Query query, Result result, Dictionary<string,List<Document>> dictionary) {
        foreach(var s in signables) {
            result.map.Add(s.sign, s.Search(query, dictionary));
        }
    }
}