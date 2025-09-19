using SqlSugar;
using System.Linq.Expressions;
using webapi.Models.BaseData;
using webapi.Dtos;
using webapi.Repository;

namespace webapi.Services;

public class BaseMatServices : IBaseMatServices 
{
    private readonly IBaseRepository<MedicalMaterial> _repo;

    private readonly SqlSugarScope _scope; // ✅ 多库支持
    public BaseMatServices(IBaseRepository<MedicalMaterial> repo, SqlSugarScope scope)
    {
        _repo = repo;
        _scope = scope;
    }

    public List<MedicalMaterial> GetAll() => _repo.GetAll();

    public int Add(MedicalMaterial entity) => _repo.Add(entity);
    public int Update(MedicalMaterial entity) => _repo.Update(entity);
    public int Delete(int id) => _repo.Delete(id);
    public MedicalMaterial GetById(int id) => _repo.GetById(id);
    public MedicalMaterial GetById(string id) => _repo.GetById(id);

    public Task<List<MedicalMaterial>> GetPagedAsync(int page, int size, Expression<Func<MedicalMaterial, bool>>? whereExpression, RefAsync<int> totalCount)
    {
        return _repo.GetPagedAsync(page, size, whereExpression, totalCount);
    }

    public Task<MedicalMaterial?> FirstOrDefaultAsync(Expression<Func<MedicalMaterial, bool>> predicate)
    {
        return _repo.FirstOrDefaultAsync(predicate);
    }

    public Task<bool> ExistsAsync(Expression<Func<MedicalMaterial, bool>> predicate)
    {
        return _repo.ExistsAsync(predicate);
    }

    public Task<List<MedicalMaterial>> GetListExpressionAsync(Expression<Func<MedicalMaterial, bool>> predicate)
    {
        return _repo.GetListExpressionAsync(predicate);
    }

    public List<MedicalMaterial> GetList(Expression<Func<MedicalMaterial, bool>> predicate)
    {
        return _repo.GetList(predicate);
    }

    public int AddRange(List<MedicalMaterial> entities)
    {
        return _repo.AddRange(entities);
    }

    public int Delete(Expression<Func<MedicalMaterial, bool>> predicate)
    {
        return _repo.Delete(predicate);
    }

    public int DeleteRange(List<int> ids)
    {
        return _repo.DeleteRange(ids);
    }

    public MedicalMaterial GetSingle(Expression<Func<MedicalMaterial, bool>> predicate)
    {
        return _repo.GetSingle(predicate);
    }

    public async Task<(List<MedicalMaterial> List, int Total)> GetMatAsyncTotal(MatLibListDto dto)
    {
        RefAsync<int> total = 0;
        
        Expression<Func<MedicalMaterial, bool>> whereExpression = u =>
            (string.IsNullOrEmpty(dto.name) || u.Name.Contains(dto.name))
            && (string.IsNullOrEmpty(dto.code) || u.SystemCode.Contains(dto.code));

        var list = await _repo.GetPagedAsync(
            dto.CurrentPage,
            dto.Size,
            whereExpression,
            total
        );
        return (list, total);
    }

    public Task<bool> AddAsync(MedicalMaterial entity)=>  _repo.AddAsync(entity);

    public Task<bool> UpdateAsync(MedicalMaterial entity) => _repo.UpdateAsync(entity);

    public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
}
