using SqlSugar;
using System.Linq.Expressions;
using webapi.Models.BaseData;
using webapi.Dtos;
using webapi.Repository;

namespace webapi.Services;

public class BaseDrugServices : IBaseDrugServices
{
    private readonly IBaseRepository<DM_DICT> _repo;

    private readonly ISqlSugarClient _db;
    public BaseDrugServices(IBaseRepository<DM_DICT> repo, ISqlSugarClient db)
    {
        _repo = repo;
        _db = db;
    }
    public async Task<int> GetMaxCode()
    {
        string orgcode = "100010";
        var entityType = typeof(DM_DICT);
        var sugarTable = (SugarTable)Attribute.GetCustomAttribute(entityType, typeof(SugarTable));
        var tableName = sugarTable?.TableName ?? entityType.Name; 
        var itemkey = tableName;
        return await _db.Ado.GetIntAsync(
            "exec [His].GetIdentityValue @itemkey=@itemkey,@orgcode=@orgcode",
            new { itemkey, orgcode }
        );
    }
    public List<DM_DICT> GetAll() => _repo.GetAll();

    public int Add(DM_DICT entity) => _repo.Add(entity);
    public int Update(DM_DICT entity) => _repo.Update(entity);
    public int Delete(int id) => _repo.Delete(id);
    public DM_DICT GetById(int id) => _repo.GetById(id);

    public Task<List<DM_DICT>> GetPagedAsync(int page, int size, Expression<Func<DM_DICT, bool>>? whereExpression, RefAsync<int> totalCount)
    {
        return _repo.GetPagedAsync(page, size, whereExpression, totalCount);
    }

    public Task<DM_DICT?> FirstOrDefaultAsync(Expression<Func<DM_DICT, bool>> predicate)
    {
        return _repo.FirstOrDefaultAsync(predicate);
    }

    public Task<bool> ExistsAsync(Expression<Func<DM_DICT, bool>> predicate)
    {
        return _repo.ExistsAsync(predicate);
    }

    public Task<List<DM_DICT>> GetListExpressionAsync(Expression<Func<DM_DICT, bool>> predicate)
    {
        return _repo.GetListExpressionAsync(predicate);
    }

    public List<DM_DICT> GetList(Expression<Func<DM_DICT, bool>> predicate)
    {
        return _repo.GetList(predicate);
    }

    public int AddRange(List<DM_DICT> entities)
    {
        return _repo.AddRange(entities);
    }

    public int Delete(Expression<Func<DM_DICT, bool>> predicate)
    {
        return _repo.Delete(predicate);
    }

    public int DeleteRange(List<int> ids)
    {
        return _repo.DeleteRange(ids);
    }

    public DM_DICT GetSingle(Expression<Func<DM_DICT, bool>> predicate)
    {
        return _repo.GetSingle(predicate);
    }

    public async Task<(List<DM_DICT> List, int Total)> GetDM_DICTAsyncTotal(DictLibListDto dto)
    {
        RefAsync<int> total = 0;

        Expression<Func<DM_DICT, bool>> whereExpression = u =>
            (string.IsNullOrEmpty(dto.cname) || u.CNAME.Contains(dto.cname))
            && (string.IsNullOrEmpty(dto.ccode) || u.CODE.Contains(dto.ccode))
            && (string.IsNullOrEmpty(dto.ClassifyCode) || u.ClassifyCode.Contains(dto.ClassifyCode));

        var list = await _repo.GetPagedAsync(
            dto.CurrentPage,
            dto.Size,
            whereExpression,
            total
        );

        return (list, total);
    }

    public DM_DICT GetById(string id) => _repo.GetById(id);

    public Task<bool> AddAsync(DM_DICT entity) => _repo.AddAsync(entity);

    public Task<bool> UpdateAsync(DM_DICT entity) => _repo.UpdateAsync(entity);

    public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);


}
