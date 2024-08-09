using FullTextSearch.Controller.ResultController.Abstraction;
using FullTextSearch.Controller.SearchController;
using FullTextSearch.Core;

namespace FullTextSearch.Controller.ResultController;

public class FilterDriver : IFilterDriver {
    public void DriveFilterer(IEnumerable<IFilter> filterers, Result result)
    { 
        filterers.ToList().ForEach(f=> result.Documents = f.Filter(result.Documents, result.DocumentsBySign));
    }
}