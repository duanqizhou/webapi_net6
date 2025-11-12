using Mapster;
using SqlSugar;
using webapi.Configs;
using webapi.Dtos;
using webapi.Dtos.Lis;
using webapi.Models.LIS;
using webapi.Repository;
using webapi.Services.Lis;

namespace webapi.Services;

public class SamUrgentvaluepromptServices : ISamUrgentvaluepromptServices
{
    private readonly IBaseRepository<SAM_UrgentValuePrompt> _repo;
    public SamUrgentvaluepromptServices(IBaseRepository<SAM_UrgentValuePrompt> repo)
    {
        _repo = repo;
    }
    public List<SAM_UrgentValuePrompt> GetAll() => _repo.GetAll(Db_LIS.Name);

    public bool InsertOrUpdate(SamUrgentvaluepromptDto samUrgentvaluepromptDto)
    {
        var entity = samUrgentvaluepromptDto.Adapt<SAM_UrgentValuePrompt>();
        return samUrgentvaluepromptDto.ID > 0
            ? _repo.Update(entity, Db_LIS.Name) > 0
            : _repo.Add(entity, Db_LIS.Name) > 0;
    }
}
