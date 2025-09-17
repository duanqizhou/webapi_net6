using SqlSugar;
using System.Linq.Expressions;
using webapi.Models;
using webapi.Repository;

namespace webapi.Services;

public class BaseCureServices : IBaseCureServices 
{
    private readonly IBaseRepository<Consulting> _repo;

    private readonly ISqlSugarClient _db;
    public BaseCureServices(IBaseRepository<Consulting> repo, ISqlSugarClient db)
    {
        _repo = repo;
        _db = db;
    }

    public List<Consulting> GetAll() => _repo.GetAll();

    public int Add(Consulting entity) => _repo.Add(entity);
    public int Update(Consulting entity) => _repo.Update(entity);
    public int Delete(int id) => _repo.Delete(id);
    public Consulting GetById(int id) => _repo.GetById(id);
    public Consulting GetById(string id) => _repo.GetById(id);

    public Task<List<Consulting>> GetPagedAsync(int page, int size, Expression<Func<Consulting, bool>>? whereExpression, RefAsync<int> totalCount)
    {
        return _repo.GetPagedAsync(page, size, whereExpression, totalCount);
    }

    public Task<Consulting?> FirstOrDefaultAsync(Expression<Func<Consulting, bool>> predicate)
    {
        return _repo.FirstOrDefaultAsync(predicate);
    }

    public Task<bool> ExistsAsync(Expression<Func<Consulting, bool>> predicate)
    {
        return _repo.ExistsAsync(predicate);
    }

    public Task<List<Consulting>> GetListExpressionAsync(Expression<Func<Consulting, bool>> predicate)
    {
        return _repo.GetListExpressionAsync(predicate);
    }

    public List<Consulting> GetList(Expression<Func<Consulting, bool>> predicate)
    {
        return _repo.GetList(predicate);
    }

    public int AddRange(List<Consulting> entities)
    {
        return _repo.AddRange(entities);
    }

    public int Delete(Expression<Func<Consulting, bool>> predicate)
    {
        return _repo.Delete(predicate);
    }

    public int DeleteRange(List<int> ids)
    {
        return _repo.DeleteRange(ids);
    }

    public Consulting GetSingle(Expression<Func<Consulting, bool>> predicate)
    {
        return _repo.GetSingle(predicate);
    }

    public async Task<(List<Consulting> List, int Total)> GetCureAsyncTotal(CureLibDto dto)
    {
        RefAsync<int> total = 0;

        var list = await _repo.GetPagedAsync(
            dto.CurrentPage,
            dto.Size,
            u => string.IsNullOrEmpty(dto.name) || u.Name.Contains(dto.name)
            && (string.IsNullOrEmpty(dto.code) || u.Code.Contains(dto.code)),
            total
        );

        return (list, total);
    }

    public Task<bool> AddAsync(Consulting entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Consulting entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}
