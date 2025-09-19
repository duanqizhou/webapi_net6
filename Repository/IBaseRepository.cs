using SqlSugar;
using System.Linq.Expressions;
using webapi.Models.BaseData;

namespace webapi.Repository;

public interface IBaseRepository<T>
{
    List<T> GetAll();
    T GetById(int id);
    T GetById(string id);

    List<T> GetList(Expression<Func<T, bool>> predicate);
    T GetSingle(Expression<Func<T, bool>> predicate);

    int Add(T entity);
    Task<bool> AddAsync(T entity);
    int AddRange(List<T> entities);
    Task<bool> AddRangeAsync(T entity);


    int Update(T entity);
    Task<bool> UpdateAsync(T entity);

    int Delete(int id);
    Task<bool> DeleteAsync(int id);
    int Delete(Expression<Func<T, bool>> predicate);
    Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate);
    int DeleteRange(List<int> ids);
    Task<bool> DeleteRangeAsync(List<int> ids);

    Task<List<T>> GetPagedAsync(int page, int size, Expression<Func<T, bool>>? whereExpression, RefAsync<int> totalCount);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> GetListExpressionAsync(Expression<Func<T, bool>> predicate);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
}
