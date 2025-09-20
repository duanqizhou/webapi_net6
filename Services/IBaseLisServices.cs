using SqlSugar;
using System.Linq.Expressions;
using webapi.Models.BaseData;

namespace webapi.Services;

public interface IBaseLisServices<T> where T : class
{
    List<T> GetAll(string DbName);
    T GetById(int id,string DbName);
    T GetById(string id,string DbName);
    List<T> GetList(Expression<Func<T, bool>> predicate,string DbName);
    T GetSingle(Expression<Func<T, bool>> predicate,string DbName);
    int Add(T entity,string DbName);
    Task<bool> AddAsync(T entity,string DbName);
    int AddRange(List<T> entities,string DbName);
    int Update(T entity,string DbName);
    Task<bool> UpdateAsync(T entity,string DbName);
    int Delete(int id,string DbName);
    Task<bool> DeleteAsync(int id,string DbName);
    int Delete(Expression<Func<T, bool>> predicate,string DbName);
    int DeleteRange(List<int> ids,string DbName);
    Task<List<T>> GetPagedAsync(int page, int size, Expression<Func<T, bool>>? whereExpression, RefAsync<int> totalCount,string DbName);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate,string DbName);
    Task<List<T>> GetListExpressionAsync(Expression<Func<T, bool>> predicate,string DbName);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate,string DbName);

}
