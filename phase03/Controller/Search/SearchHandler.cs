public class SearchHandler // get query and result handler as property or as parameter
{
    public Dictionary<string, List<Document>> dictionary{ get; set;}
    public List<char> signs = new List<char>{'+', '-', ' '};
    public List<ISearcher> searchers = new List<ISearcher>(){new MinusSearcher(), new PlusSearcher(), new NoSignedSearcher()}; 
    public List<IFilterer> filterables = new List<IFilterer>(){new NoSignedFilterer(), new PlusFilterer(), new MinusFilterer()};
    public SearcherDriver searcherDriver = new SearcherDriver();
    public FiltererDirver filteretDriver = new FiltererDirver();
    public QueryExtractor queryExtractor = new QueryExtractor(new QueryProcessor());
    public SearchHandler(Dictionary<string, List<Document>> invertedIndex)
    {
        this.dictionary = invertedIndex;
    }

    public Search Search(string input)
    {
        var query = new Query(input);
        var result = new Result();
        var search = new Search(query, result);    

        queryExtractor.ExtractQuery(query, signs);
        searcherDriver.DriveSearch(searchers, query, result, dictionary);
        filteretDriver.DriveFilterer(filterables, result);

        return search;
    }
}