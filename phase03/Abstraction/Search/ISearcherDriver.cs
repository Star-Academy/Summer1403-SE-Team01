using System.Reflection.Metadata.Ecma335;

public interface ISearcherDriver {
    public void DriveSearch(IEnumerable<ISearcher> searchers, Query query, Result result, Dictionary<string,List<Document>> invertedIndex);
}