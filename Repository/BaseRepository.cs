using webapi.Repository;
using webapi.Models;
using webapi.Services;
using SqlSugar;
using System.Linq.Expressions;

namespace webapi.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
{
    private readonly ISqlSugarClient _db;
    public BaseRepository(ISqlSugarClient db)
    {
        _db = db;
    }


    public List<T> GetAll() => _db.Queryable<T>().ToList();
    public List<T> GetList(Expression<Func<T, bool>> predicate) => _db.Queryable<T>().Where(predicate).ToList();

    public T GetSingle(Expression<Func<T, bool>> predicate) => _db.Queryable<T>().Where(predicate).First(); //联合主键查询
    public T GetById(int id) => _db.Queryable<T>().InSingle(id); //单主键查询
    public T GetById(string id) => _db.Queryable<T>().InSingle(id); //单主键查询



    public int Add(T entity) => _db.Insertable<T>(entity).ExecuteReturnIdentity();
    public async Task<bool> AddAsync(T entity) => await _db.Insertable<T>(entity).ExecuteCommandAsync() > 0;
    public int AddRange(List<T> entities) => _db.Insertable(entities).ExecuteCommand();
    public async Task<bool> AddRangeAsync(T entity) => await _db.Insertable<T>(entity).ExecuteCommandAsync() > 0;


    public int Update(T entity) => _db.Updateable(entity).ExecuteCommand();
    public async Task<bool> UpdateAsync(T entity) => await _db.Updateable(entity).ExecuteCommandAsync() > 0;


    public int Delete(int id) => _db.Deleteable<T>().In(id).ExecuteCommand();
    public async Task<bool> DeleteAsync(int id) => await _db.Deleteable<T>().In(id).ExecuteCommandAsync() > 0;
    public int Delete(Expression<Func<T, bool>> predicate) => _db.Deleteable<T>().Where(predicate).ExecuteCommand();
    public async Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate) => await _db.Deleteable<T>().Where(predicate).ExecuteCommandAsync() > 0;
    public int DeleteRange(List<int> ids) => _db.Deleteable<T>().In(ids).ExecuteCommand();
    public async Task<bool> DeleteRangeAsync(List<int> ids) => await _db.Deleteable<T>().In(ids).ExecuteCommandAsync() > 0;
    /// <summary>
    /// 分页查询
    /// </summary>
    public async Task<List<T>> GetPagedAsync(int page, int size, Expression<Func<T, bool>>? whereExpression, RefAsync<int> totalCount)
    {
        var query = _db.Queryable<T>();

        if (whereExpression != null)
        {
            query = query.Where(whereExpression);
        }

        return await query.ToPageListAsync(page, size, totalCount);
    }

    /// <summary>
    /// 获取单个对象
    /// </summary>
    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _db.Queryable<T>().FirstAsync(predicate);
    }
    /// <summary>
    /// 获取过滤对象
    /// </summary>
    public async Task<List<T>> GetListExpressionAsync(Expression<Func<T, bool>> predicate = null)
    {
        var query = _db.Queryable<T>().Where(predicate);
        return await query.ToListAsync();
    }
    /// <summary>
    /// 判断是否存在
    /// </summary>
    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await _db.Queryable<T>().AnyAsync(predicate);
    }

    Task<List<T>> IBaseRepository<T>.GetListExpressionAsync(Expression<Func<T, bool>> predicate)
    {
        return GetListExpressionAsync(predicate);
    }

}
