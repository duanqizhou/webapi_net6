using System.Linq.Expressions;
using webapi.Configs;
using webapi.Models.LIS;
using webapi.Repository;

namespace webapi.Services;

public class SamInstrServices : ISamInstrServices
{
    private readonly IBaseRepository<SAM_INSTR> _repo;
    public SamInstrServices(IBaseRepository<SAM_INSTR> repo)
    {
        _repo = repo;
    }
    public List<SAM_INSTR> GetAll() => _repo.GetAll(Db_LIS.Name);

}
