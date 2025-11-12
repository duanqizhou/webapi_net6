using SqlSugar;
using System.Linq.Expressions;
using webapi.Configs;
using webapi.Dtos;
using webapi.Models.LIS;
using webapi.Repository;
using webapi.Services.Lis;

namespace webapi.Services;

public class SamSampleTypeServices : ISamSampleTypeServices
{
    private readonly IBaseRepository<SAM_SAMPLE_TYPE> _repo;
    public SamSampleTypeServices(IBaseRepository<SAM_SAMPLE_TYPE> repo)
    {
        _repo = repo;
    }
    public List<SAM_SAMPLE_TYPE> GetAll() => _repo.GetAll(Db_LIS.Name);

    public Task<List<SAM_SAMPLE_TYPE>> GetListExpressionAsync(Expression<Func<SAM_SAMPLE_TYPE, bool>> predicate)
    => _repo.GetListExpressionAsync(predicate,Db_LIS.Name);
}
