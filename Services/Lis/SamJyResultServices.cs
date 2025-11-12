using SqlSugar;
using webapi.Dtos;
using webapi.Models.LIS;
using webapi.Repository;
using webapi.Services.Lis;

namespace webapi.Services;

public class SamJyResultServices : ISamJyResultServices
{
    private readonly IBaseRepository<SAM_JY_RESULT> _repo;
    public SamJyResultServices(IBaseRepository<SAM_JY_RESULT> repo)
    {
        _repo = repo;
    }
    public List<SAM_JY_RESULT> GetAll() => _repo.GetAll();
}
