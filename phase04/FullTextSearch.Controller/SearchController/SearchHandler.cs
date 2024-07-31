namespace FullTextSearch.Controller.SearchController;

public class SearchHandler // get query and result handler as property or as parameter
{
    /*
    public Dictionary<string, List<FullTextSearch.Model.Document>> dictionary{ get; set;}
    //public List<char> signs = new List<char>{'+', '-', ' '};
    public List<ISearcher> searchers = new List<ISearcher>(){new MinusSearcher(), new PlusSearcher(), new NoSignedSearcher()}; 
    public List<IFilterer> filterables = new List<IFilterer>(){new NoSignedFilterer(), new PlusFilterer(), new MinusFilterer()};
    public ISearcherDriver searcherDriver;
    public IFiltererDriver filteretDriver;
    public IQueryExtractor queryExtractor;
    public SearchHandler(Dictionary<string, List<InvertedIndex.Model.Document>> invertedIndex, ISearcherDriver searcherDriver, IFiltererDriver filtererDriver, IQueryExtractor queryExtractor)
    {
        this.dictionary = invertedIndex;
        this.searcherDriver = searcherDriver;
        this.filteretDriver = filtererDriver;
        this.queryExtractor = queryExtractor;
    }

    public InvertedIndex.Model.Search Search(string input)
    {

        var query = queryExtractor.Extract(input);
        var result = new InvertedIndex.Model.Result();
        var search = new InvertedIndex.Model.Search(query, result);
        
        searcherDriver.DriveSearch(searchers, query, result, dictionary);
        filteretDriver.DriveFilterer(filterables, result);

        return search;
    }*/
}