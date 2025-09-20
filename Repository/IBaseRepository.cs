using SqlSugar;
using System.Linq.Expressions;
namespace webapi.Repository;

public interface IBaseRepository<T>
{

    List<T> GetAll(string dbName = "BaseData");
    T GetById(int id, string dbName = "BaseData");
    T GetById(string id, string dbName = "BaseData");
    List<T> GetList(Expression<Func<T, bool>> predicate, string dbName = "BaseData");
    T GetSingle(Expression<Func<T, bool>> predicate, string dbName = "BaseData");
    int Add(T entity, string dbName = "BaseData");
    Task<bool> AddAsync(T entity, string dbName = "BaseData");
    int AddRange(List<T> entities, string dbName = "BaseData");
    Task<bool> AddRangeAsync(List<T> entities, string dbName = "BaseData");
    int Update(T entity, string dbName = "BaseData");
    Task<bool> UpdateAsync(T entity, string dbName = "BaseData");
    int Delete(int id, string dbName = "BaseData");
    Task<bool> DeleteAsync(int id, string dbName = "BaseData");
    int Delete(Expression<Func<T, bool>> predicate, string dbName = "BaseData");
    Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate, string dbName = "BaseData");
    int DeleteRange(List<int> ids, string dbName = "BaseData");
    Task<bool> DeleteRangeAsync(List<int> ids, string dbName = "BaseData");
    Task<List<T>> GetPagedAsync(int page, int size, Expression<Func<T, bool>>? whereExpression, RefAsync<int> totalCount, string dbName = "BaseData");
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, string dbName = "BaseData");
    Task<List<T>> GetListExpressionAsync(Expression<Func<T, bool>> predicate, string dbName = "BaseData");
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, string dbName = "BaseData");
}
