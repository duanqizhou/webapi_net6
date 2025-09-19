using SqlSugar;
using System.Linq.Expressions;
using webapi.Models.BaseData;
using webapi.Dtos;
using webapi.Repository;

namespace webapi.Services;

public class UserServices : IUserServices
{
    private readonly IBaseRepository<ACCOUNTS> _repo;
    private readonly SqlSugarScope _scope; // ✅ 多库支持

    public UserServices(IBaseRepository<ACCOUNTS> repo, SqlSugarScope scope)
    {
        _repo = repo;
        _scope = scope ?? throw new ArgumentNullException(nameof(scope));
    }

    // 默认库操作
    public List<ACCOUNTS> GetAll() => _repo.GetAll();
    public int Add(ACCOUNTS entity) => _repo.Add(entity);
    public int Update(ACCOUNTS entity) => _repo.Update(entity);
    public int Delete(int id) => _repo.Delete(id);
    public ACCOUNTS GetById(int id) => _repo.GetById(id);
    public ACCOUNTS GetById(string id) => _repo.GetById(id);

    public Task<List<ACCOUNTS>> GetPagedAsync(int page, int size, Expression<Func<ACCOUNTS, bool>>? whereExpression, RefAsync<int> totalCount)
        => _repo.GetPagedAsync(page, size, whereExpression, totalCount);

    public Task<ACCOUNTS?> FirstOrDefaultAsync(Expression<Func<ACCOUNTS, bool>> predicate)
        => _repo.FirstOrDefaultAsync(predicate);

    public Task<bool> ExistsAsync(Expression<Func<ACCOUNTS, bool>> predicate)
        => _repo.ExistsAsync(predicate);

    public async Task<(List<ACCOUNTS> List, int Total)> GetUsersAsyncTotal(UserQueryDto dto)
    {
        RefAsync<int> total = 0;

        var list = await _repo.GetPagedAsync(
            dto.CurrentPage,
            dto.Size,
            u => string.IsNullOrEmpty(dto.Username) || u.EMPID.Contains(dto.Username),
            total
        );

        return (list, total);
    }

    public Task<List<ACCOUNTS>> GetListExpressionAsync(Expression<Func<ACCOUNTS, bool>> predicate)
        => _repo.GetListExpressionAsync(predicate);

    public List<ACCOUNTS> GetList(Expression<Func<ACCOUNTS, bool>> predicate)
        => _repo.GetList(predicate);

    public int AddRange(List<ACCOUNTS> entities) => _repo.AddRange(entities);
    public int Delete(Expression<Func<ACCOUNTS, bool>> predicate) => _repo.Delete(predicate);
    public int DeleteRange(List<int> ids) => _repo.DeleteRange(ids);
    public ACCOUNTS GetSingle(Expression<Func<ACCOUNTS, bool>> predicate) => _repo.GetSingle(predicate);

    /// <summary>
    /// 获取最大 LoginId（默认库 BaseData）
    /// </summary>
    public async Task<int> GetMaxLoginIdAsync()
    {
        var maxLoginIdStr = await _scope.GetConnectionScope("BaseData")
                                        .Queryable<ACCOUNTS>()
                                        .MaxAsync(u => u.EMPID);
        int.TryParse(maxLoginIdStr, out int maxId);
        return maxId + 1;
    }

    public Task<bool> AddAsync(ACCOUNTS entity) => _repo.AddAsync(entity);
    public Task<bool> UpdateAsync(ACCOUNTS entity) => _repo.UpdateAsync(entity);
    public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);

    // ✅ 如果将来需要操作其他库
    public ISqlSugarClient GetDb(string dbId) => _scope.GetConnectionScope(dbId);
}
