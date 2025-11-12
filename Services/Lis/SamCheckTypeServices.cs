using System.Linq.Expressions;
using webapi.Configs;
using webapi.Models.LIS;
using webapi.Repository;

namespace webapi.Services;

public class SamCheckTypeServices : ISamCheckTypeServices
{
    private readonly IBaseRepository<SAM_CHECK_TYPE> _repo;
    public SamCheckTypeServices(IBaseRepository<SAM_CHECK_TYPE> repo)
    {
        _repo = repo;
    }
    public List<SAM_CHECK_TYPE> GetAll() => _repo.GetAll(Db_LIS.Name);

    public Task<List<SAM_CHECK_TYPE>> GetListExpressionAsync(Expression<Func<SAM_CHECK_TYPE, bool>> predicate)
    => _repo.GetListExpressionAsync(predicate, Db_LIS.Name);
}
