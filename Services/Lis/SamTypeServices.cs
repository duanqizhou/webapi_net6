using System.Linq.Expressions;
using webapi.Configs;
using webapi.Models.LIS;
using webapi.Repository;

namespace webapi.Services;

public class SamTypeServices : ISamTypeServices
{
    private readonly IBaseRepository<SAM_TYPE> _repo;
    public SamTypeServices(IBaseRepository<SAM_TYPE> repo)
    {
        _repo = repo;
    }
    public List<SAM_TYPE> GetAll() => _repo.GetAll(Db_LIS.Name);

    public Task<List<SAM_TYPE>> GetListExpressionAsync(List<string> types)
    {
        var targetTypes = types?.Any() == true ? types : new List<string>();
        Expression<Func<SAM_TYPE, bool>> whereExpression = u => targetTypes.Contains(u.ftype);
        return _repo.GetListExpressionAsync(whereExpression, Db_LIS.Name);
    }

}
