using FullTextSearch.Core;

namespace FullTextSearch.Controller.ResultController.Abstraction;

public interface IFilterDriver {
    void DriveFilterer(IEnumerable<IFilter> filterers, Result result);
}