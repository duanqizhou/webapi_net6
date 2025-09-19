using SqlSugar;
using System.Linq.Expressions;
using webapi.Models.BaseData;
using webapi.Dtos;
using webapi.Repository;

namespace webapi.Services;

public class BaseCureServices : IBaseCureServices 
{
    private readonly IBaseRepository<Consulting> _repo;

    private readonly SqlSugarScope _scope; // ✅ 多库支持

    public BaseCureServices(IBaseRepository<Consulting> repo, SqlSugarScope scope)
    {
        _repo = repo;
        _scope = scope ?? throw new ArgumentNullException(nameof(scope));
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

    public async Task<(List<Consulting> List, int Total)> GetCureAsyncTotal(CureLibListDto dto)
    {
        RefAsync<int> total = 0;

        Expression<Func<Consulting, bool>> whereExpression = u =>
            (string.IsNullOrEmpty(dto.name) || u.Name.Contains(dto.name))
            && (string.IsNullOrEmpty(dto.code) || u.GuoJiaBianMa.Contains(dto.code));

        var list = await _repo.GetPagedAsync(
            dto.CurrentPage,
            dto.Size,
            whereExpression,
            total
        );

        return (list, total);
    }

    public Task<bool> AddAsync(Consulting entity) => _repo.AddAsync(entity);

    public Task<bool> UpdateAsync(Consulting entity) => _repo.UpdateAsync(entity);

    public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
}
