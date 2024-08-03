using FullTextSearch.Controller.ResultController.Abstraction;
using FullTextSearch.Core;

namespace FullTextSearch.Controller.ResultController;

public interface IFilterDriver {
    public void DriveFilterer(IEnumerable<IFilter> filterers, Result result);
}