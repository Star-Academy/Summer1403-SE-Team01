using FullTextSearch.Controller.ResultController.Abstraction;
using FullTextSearch.Core;

namespace FullTextSearch.Controller.ResultController;

public interface IFilterDriver {
    void DriveFilterer(IEnumerable<IFilter> filterers, Result result);
}