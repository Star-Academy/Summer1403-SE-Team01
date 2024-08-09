namespace FullTextSearch.Controller.QueryController.Abstraction;

public interface IQueryDirector
{
    void Construct(string text, IQueryBuilder queryBuilder);
}