namespace FullTextSearch.Controller.QueryController.Abstraction;

public interface IQueryDirector
{
    void Construct(IQueryBuilder queryBuilder);
}