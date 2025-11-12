using SqlSugar;
using webapi.Configs;
using webapi.Dtos;
using webapi.Models.LIS;
using webapi.Repository;
using webapi.Services.Lis;

namespace webapi.Services;

public class WwfPositionServices : IWwfPositionServices
{
    private readonly IBaseRepository<WWF_POSITION> _repo;
    public WwfPositionServices(IBaseRepository<WWF_POSITION> repo)
    {
        _repo = repo;
    }
    public List<WWF_POSITION> GetAll() => _repo.GetAll(Db_LIS.Name);
}
