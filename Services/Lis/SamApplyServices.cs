using SqlSugar;
using webapi.Configs;
using webapi.Dtos;
using webapi.Models.LIS;
using webapi.Repository;
using webapi.Services.Lis;

namespace webapi.Services;

public class SamApplyServices : ISamApplyServices
{
    private readonly IBaseRepository<SAM_APPLY> _repo;
    public SamApplyServices(IBaseRepository<SAM_APPLY> repo)
    {
        _repo = repo;
    }
    public List<SAM_APPLY> GetAll() => _repo.GetAll(Db_LIS.Name); 
}
