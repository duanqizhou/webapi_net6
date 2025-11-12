using SqlSugar;
using webapi.Configs;
using webapi.Dtos;
using webapi.Models.LIS;
using webapi.Repository;
using webapi.Services.Lis;

namespace webapi.Services;

public class WwfSysServices : IWwfSysServices
{
    private readonly IBaseRepository<WWF_SYS> _repo;
    public WwfSysServices(IBaseRepository<WWF_SYS> repo)
    {
        _repo = repo;
    }
    public List<WWF_SYS> GetAll() => _repo.GetAll(Db_LIS.Name);
}
