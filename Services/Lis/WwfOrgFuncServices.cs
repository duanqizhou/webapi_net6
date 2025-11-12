using SqlSugar;
using webapi.Configs;
using webapi.Dtos;
using webapi.Models.LIS;
using webapi.Repository;
using webapi.Services.Lis;

namespace webapi.Services;

public class WwfOrgFuncServices : IWwfOrgFuncServices
{
    private readonly IBaseRepository<WWF_ORG_FUNC> _repo;
    public WwfOrgFuncServices(IBaseRepository<WWF_ORG_FUNC> repo)
    {
        _repo = repo;
    }
    public List<WWF_ORG_FUNC> GetAll() => _repo.GetAll(Db_LIS.Name);
}
