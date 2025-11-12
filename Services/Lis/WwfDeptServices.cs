using SqlSugar;
using webapi.Configs;
using webapi.Dtos;
using webapi.Models.LIS;
using webapi.Repository;
using webapi.Services.Lis;

namespace webapi.Services;

public class WwfDeptServices : IWwfDeptServices
{
    private readonly IBaseRepository<WWF_DEPT> _repo;
    public WwfDeptServices(IBaseRepository<WWF_DEPT> repo)
    {
        _repo = repo;
    }
    public List<WWF_DEPT> GetAll() => _repo.GetAll(Db_LIS.Name);
}
