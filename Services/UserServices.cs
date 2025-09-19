using SqlSugar;
using System.Linq.Expressions;
using webapi.Models.BaseData;
using webapi.Dtos;
using webapi.Repository;

namespace webapi.Services;

public class UserServices : IUserServices
{
    private readonly IBaseRepository<ACCOUNTS> _repo;

    private readonly ISqlSugarClient _db;
    public UserServices(IBaseRepository<ACCOUNTS> repo, ISqlSugarClient db)
    {
        _repo = repo;
        _db = db;
    }

    public List<ACCOUNTS> GetAll() => _repo.GetAll();

    public int Add(ACCOUNTS entity) => _repo.Add(entity);
    public int Update(ACCOUNTS entity) => _repo.Update(entity);
    public int Delete(int id) => _repo.Delete(id);
    public ACCOUNTS GetById(int id) => _repo.GetById(id);
    public ACCOUNTS GetById(string id) => _repo.GetById(id);

    public Task<List<ACCOUNTS>> GetPagedAsync(int page, int size, Expression<Func<ACCOUNTS, bool>>? whereExpression, RefAsync<int> totalCount)
    {
        return _repo.GetPagedAsync(page, size, whereExpression, totalCount);
    }

    public Task<ACCOUNTS?> FirstOrDefaultAsync(Expression<Func<ACCOUNTS, bool>> predicate)
    {
        return _repo.FirstOrDefaultAsync(predicate);
    }

    public Task<bool> ExistsAsync(Expression<Func<ACCOUNTS, bool>> predicate)
    {
        return _repo.ExistsAsync(predicate);
    }
    public async Task<(List<ACCOUNTS> List, int Total)> GetUsersAsyncTotal(UserQueryDto dto)
    {
        RefAsync<int> total = 0;

        var list = await _repo.GetPagedAsync(
            dto.CurrentPage,
            dto.Size,
            u => (string.IsNullOrEmpty(dto.Username) || u.EMPID.Contains(dto.Username)),
            total
        );

        return (list, total);
    }

    public Task<List<ACCOUNTS>> GetListExpressionAsync(Expression<Func<ACCOUNTS, bool>> predicate)
    {
        return _repo.GetListExpressionAsync(predicate);
    }

    public List<ACCOUNTS> GetList(Expression<Func<ACCOUNTS, bool>> predicate)
    {
        return _repo.GetList(predicate);
    }

    public int AddRange(List<ACCOUNTS> entities)
    {
        return _repo.AddRange(entities);
    }

    public int Delete(Expression<Func<ACCOUNTS, bool>> predicate)
    {
        return _repo.Delete(predicate);
    }

    public int DeleteRange(List<int> ids)
    {
        return _repo.DeleteRange(ids);
    }

    public ACCOUNTS GetSingle(Expression<Func<ACCOUNTS, bool>> predicate)
    {
        return _repo.GetSingle(predicate);
    }

    public Task<int> GetMaxLoginIdAsync()
    {
        var maxLoginId = _db.Queryable<ACCOUNTS>().MaxAsync(u => u.EMPID);
        int maxId = 0;
        int.TryParse(maxLoginId.Result, out maxId);
        return Task.FromResult(maxId + 1);
    }

    public Task<bool> AddAsync(ACCOUNTS entity)
    {
        return _repo.AddAsync(entity);
    }

    public Task<bool> UpdateAsync(ACCOUNTS entity)
    {
        return _repo.UpdateAsync(entity);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return _repo.DeleteAsync(id);
    }
}
