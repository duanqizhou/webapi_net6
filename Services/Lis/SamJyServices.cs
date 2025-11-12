using SqlSugar;
using webapi.Dtos;
using webapi.Models.LIS;
using webapi.Repository;
using webapi.Services.Lis;

namespace webapi.Services;

public class SamJyServices : ISamJyServices
{
    private readonly IBaseRepository<SAM_JY> _repo;
    public SamJyServices(IBaseRepository<SAM_JY> repo)
    {
        _repo = repo;
    }
    public List<SAM_JY> GetAll() => _repo.GetAll();
}
