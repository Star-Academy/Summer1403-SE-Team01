using FullTextSearch.Core;

namespace FullTextSearch.Controller.ResultController;

public class FiltererDriver : IFiltererDriver {
    public void DriveFilterer(IEnumerable<IFilterer> filterers, Result result)
    {
        filterers.ToList().ForEach(r=>r.Filter(result));
    }
}