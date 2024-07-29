using System.Data.Common;

public class SearchHandler // get query and result handler as property or as parameter
{
    public Dictionary<string, List<Document>> dictionary{ get; set;}
    public List<char> signs = new List<char>{'+', '-', ' '};
    public List<ISearcher> searchers = new List<ISearcher>(){new MinusSearcher(), new PlusSearcher(), new NoSignedSearcher()}; 
    public List<IFilterer> filterables = new List<IFilterer>(){new NoSignedFilterer(), new PlusFilterer(), new MinusFilterer()};
    public ISearcherDriver searcherDriver;
    public IFiltererDriver filteretDriver;
    public IQueryExtractor queryExtractor;
    public SearchHandler(Dictionary<string, List<Document>> invertedIndex, ISearcherDriver searcherDriver, IFiltererDriver filtererDriver, IQueryExtractor queryExtractor)
    {
        this.dictionary = invertedIndex;
        this.searcherDriver = searcherDriver;
        this.filteretDriver = filtererDriver;
        this.queryExtractor = queryExtractor;
    }

    public Search Search(string input)
    {
        var query = new Query(input);
        var result = new Result();
        var search = new Search(query, result);    

        queryExtractor.Extract(query, signs);
        searcherDriver.DriveSearch(searchers, query, result, dictionary);
        filteretDriver.DriveFilterer(filterables, result);

        return search;
    }
}