using SqlSugar;
using webapi.Dtos;
using webapi.Models.LIS;
using webapi.Repository;
using webapi.Services.Lis;

namespace webapi.Services;

public class GroupServices : IGroupServices
{
    private readonly IBaseRepository<GROUP> _repo;
    public GroupServices(IBaseRepository<GROUP> repo)
    {
        _repo = repo;
    }
    public List<GROUP> GetAll() => _repo.GetAll();
}
