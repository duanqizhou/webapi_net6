using SqlSugar;
using webapi.Configs;
using webapi.Dtos;
using webapi.Models.LIS;
using webapi.Repository;
using webapi.Services.Lis;

namespace webapi.Services;

public class WwfPersonServices : IWwfPersonServices
{
    private readonly IBaseRepository<WWF_PERSON> _repo;
    public WwfPersonServices(IBaseRepository<WWF_PERSON> repo)
    {
        _repo = repo;
    }
    public Task<WWF_PERSON> BllUserPassOk(string fperson_id, string fpass) => _repo.FirstOrDefaultAsync(fd => fd.fpass == fpass && fd.fperson_id == fperson_id);

    public List<WWF_PERSON> GetAll() => _repo.GetAll(Db_LIS.Name);
    WWF_PERSON IWwfPersonServices.GetById(string id, string DbName) => _repo.GetById(id,DbName);
}
