using SqlSugar;
using System.Linq.Expressions;

namespace webapi.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
{
    private readonly Func<string, ISqlSugarClient> _dbFactory;

    public BaseRepository(Func<string, ISqlSugarClient> dbFactory)
    {
        _dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
    }

    /// <summary>
    /// 获取指定库的 ISqlSugarClient
    /// </summary>
    private ISqlSugarClient Db(string dbName = "BaseData") => _dbFactory(dbName);

    public List<T> GetAll(string dbName = "BaseData") => Db(dbName).Queryable<T>().ToList();

    public List<T> GetList(Expression<Func<T, bool>> predicate, string dbName = "BaseData")
        => Db(dbName).Queryable<T>().Where(predicate).ToList();

    public T GetSingle(Expression<Func<T, bool>> predicate, string dbName = "BaseData")
        => Db(dbName).Queryable<T>().Where(predicate).First();

    public T GetById(int id, string dbName = "BaseData")
        => Db(dbName).Queryable<T>().InSingle(id);

    public T GetById(string id, string dbName = "BaseData")
        => Db(dbName).Queryable<T>().InSingle(id);

    public int Add(T entity, string dbName = "BaseData")
        => Db(dbName).Insertable(entity).ExecuteReturnIdentity();

    public async Task<bool> AddAsync(T entity, string dbName = "BaseData")
        => await Db(dbName).Insertable(entity).ExecuteCommandAsync() > 0;

    public int AddRange(List<T> entities, string dbName = "BaseData")
        => Db(dbName).Insertable(entities).ExecuteCommand();

    public async Task<bool> AddRangeAsync(List<T> entities, string dbName = "BaseData")
        => await Db(dbName).Insertable(entities).ExecuteCommandAsync() > 0;

    public int Update(T entity, string dbName = "BaseData")
        => Db(dbName).Updateable(entity).ExecuteCommand();

    public async Task<bool> UpdateAsync(T entity, string dbName = "BaseData")
        => await Db(dbName).Updateable(entity).ExecuteCommandAsync() > 0;

    public int Delete(int id, string dbName = "BaseData")
        => Db(dbName).Deleteable<T>().In(id).ExecuteCommand();

    public async Task<bool> DeleteAsync(int id, string dbName = "BaseData")
        => await Db(dbName).Deleteable<T>().In(id).ExecuteCommandAsync() > 0;

    public int Delete(Expression<Func<T, bool>> predicate, string dbName = "BaseData")
        => Db(dbName).Deleteable<T>().Where(predicate).ExecuteCommand();

    public async Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate, string dbName = "BaseData")
        => await Db(dbName).Deleteable<T>().Where(predicate).ExecuteCommandAsync() > 0;

    public int DeleteRange(List<int> ids, string dbName = "BaseData")
        => Db(dbName).Deleteable<T>().In(ids).ExecuteCommand();

    public async Task<bool> DeleteRangeAsync(List<int> ids, string dbName = "BaseData")
        => await Db(dbName).Deleteable<T>().In(ids).ExecuteCommandAsync() > 0;

    public async Task<List<T>> GetPagedAsync(int page, int size, Expression<Func<T, bool>>? whereExpression, RefAsync<int> totalCount, string dbName = "BaseData")
    {
        var query = Db(dbName).Queryable<T>();
        if (whereExpression != null) query = query.Where(whereExpression);
        return await query.ToPageListAsync(page, size, totalCount);
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, string dbName = "BaseData")
        => await Db(dbName).Queryable<T>().FirstAsync(predicate);

    public async Task<List<T>> GetListExpressionAsync(Expression<Func<T, bool>> predicate, string dbName = "BaseData")
        => await Db(dbName).Queryable<T>().Where(predicate).ToListAsync();

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, string dbName = "BaseData")
        => await Db(dbName).Queryable<T>().AnyAsync(predicate);
}
