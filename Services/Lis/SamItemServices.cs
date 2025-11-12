using System.Linq.Expressions;
using webapi.Configs;
using webapi.Models.LIS;
using webapi.Repository;

namespace webapi.Services;

public class SamItemServices : ISamItemServices
{
    private readonly IBaseRepository<SAM_ITEM> _repo;
    public SamItemServices(IBaseRepository<SAM_ITEM> repo)
    {
        _repo = repo;
    }
    public List<SAM_ITEM> GetAll() => _repo.GetAll(Db_LIS.Name);

    public Task<List<SAM_ITEM>> GetListExpressionAsync(Expression<Func<SAM_ITEM, bool>> predicate)
    => _repo.GetListExpressionAsync(predicate, Db_LIS.Name);
}
