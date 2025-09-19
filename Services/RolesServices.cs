using SqlSugar;
using System.Linq.Expressions;
using webapi.Models.BaseData;
using webapi.Repository;

namespace webapi.Services;

public class ROLESServices : IROLESServices
{
    private readonly IBaseRepository<ROLES> _repo;

    public ROLESServices(IBaseRepository<ROLES> repo)
    {
        _repo = repo;
    }

    public List<ROLES> GetAll() => _repo.GetAll();

    public int Add(ROLES entity) => _repo.Add(entity);
    public int Update(ROLES entity) => _repo.Update(entity);
    public int Delete(int id) => _repo.Delete(id);
    public ROLES GetById(int id) => _repo.GetById(id);
    public ROLES GetById(string id) => _repo.GetById(id);

    public Task<List<ROLES>> GetPagedAsync(int page, int size, Expression<Func<ROLES, bool>>? whereExpression, RefAsync<int> totalCount)
    {
        return _repo.GetPagedAsync(page, size, whereExpression, totalCount);
    }

    public Task<ROLES?> FirstOrDefaultAsync(Expression<Func<ROLES, bool>> predicate)
    {
        return _repo.FirstOrDefaultAsync(predicate);
    }

    public Task<bool> ExistsAsync(Expression<Func<ROLES, bool>> predicate)
    {
        return _repo.ExistsAsync(predicate);
    }

    public Task<List<ROLES>> GetListExpressionAsync(Expression<Func<ROLES, bool>> predicate)
    {
        return _repo.GetListExpressionAsync(predicate);
    }

    public List<ROLES> GetList(Expression<Func<ROLES, bool>> predicate)
    {
        return _repo.GetList(predicate);
    }

    public int AddRange(List<ROLES> entities)
    {
        return _repo.AddRange(entities);
    }

    public int Delete(Expression<Func<ROLES, bool>> predicate)
    {
        return _repo.Delete(predicate);
    }

    public int DeleteRange(List<int> ids)
    {
        return _repo.DeleteRange(ids);
    }

    public ROLES GetSingle(Expression<Func<ROLES, bool>> predicate)
    {
        return _repo.GetSingle(predicate);
    }

    public Task<bool> AddAsync(ROLES entity)
    {
        return _repo.AddAsync(entity);
    }

    public Task<bool> UpdateAsync(ROLES entity)
    {
        return _repo.UpdateAsync(entity);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return _repo.DeleteAsync(id);
    }
}
