using FullTextSearch.Core;

namespace FullTextSearch.Controller.ResultController;

public interface IFiltererDriver {
    public void DriveFilterer(IEnumerable<IFilterer> filterers, Result result);
}