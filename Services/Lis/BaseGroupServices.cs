using SqlSugar;
using webapi.Dtos;
using webapi.Models.LIS;
using webapi.Repository;
using webapi.Services.Lis;

namespace webapi.Services;

public class BaseGroupServices : IBaseGroupServices
{
    private readonly IBaseRepository<BASEGROUP> _repo;
    public BaseGroupServices(IBaseRepository<BASEGROUP> repo)
    {
        _repo = repo;
    }
    public List<BASEGROUP> GetAll() => _repo.GetAll();
}
