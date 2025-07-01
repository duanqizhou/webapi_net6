using webapi.Repository;
using webapi.Models;
using webapi.Services;
using SqlSugar;

namespace webapi.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
{
    private readonly ISqlSugarClient _db;
    public BaseRepository(ISqlSugarClient db)
    {
        _db = db;
    }

    public List<T> GetAll() => _db.Queryable<T>().ToList();

    public int Add(T entity) => _db.Insertable<T>(entity).ExecuteReturnIdentity();

    public int Update(T entity) => _db.Updateable(entity).ExecuteCommand();

    public int Delete(int id) => _db.Deleteable<T>().In(id).ExecuteCommand();
    public T GetById(int id) => _db.Queryable<T>().InSingle(id);
}
