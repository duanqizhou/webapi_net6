using SqlSugar;
using webapi.Configs;
using webapi.Dtos;
using webapi.Dtos.Lis;
using webapi.Models.LIS;
using webapi.Repository;
using webapi.Services.Lis;
using static Dm.net.buffer.ByteArrayBuffer;

namespace webapi.Services;

public class WwfFuncServices : IWwfFuncServices
{
    private readonly IBaseRepository<WWF_FUNC> _repo;
    private readonly Func<string, ISqlSugarClient> _dbFactory;
    public WwfFuncServices(IBaseRepository<WWF_FUNC> repo, Func<string, ISqlSugarClient> dbFactory)
    {
        _repo = repo;
        _dbFactory = dbFactory;
    }
    public List<WWF_FUNC> GetAll() => _repo.GetAll(Db_LIS.Name);

    public async Task<List<WwfFuncOrgDto>> GetFuncsByOrgIdAsync(string orgId)
    {
        var lisDb = _dbFactory("LIS");

        return await lisDb.Queryable<WWF_FUNC, WWF_ORG_FUNC>(
                (t1, t2) => t1.ffunc_id == t2.ffunc_id
            )
            .Where((t1, t2) => t1.fuse_flag == 1 && t2.forg_id == orgId)
            .Select((t1, t2) => new WwfFuncOrgDto
            {
                forg_id = t2.forg_id,
                ffunc_id = t1.ffunc_id,
                ftool_flag = t2.ftool_flag,
                fgrzm_flag = t2.fgrzm_flag,
                fp_id = t1.fp_id,
                fname = t1.fname,
                fname_e = t1.fname_e,
                ficon = t1.ficon,
                ffunc_winform = t1.ffunc_winform,
                ffunc_web = t1.ffunc_web,
                forder_by = t1.forder_by,
                fuse_flag = t1.fuse_flag,
                fhelp = t1.fhelp,
                fcj_flag = t1.fcj_flag,
                fcj_name = t1.fcj_name,
                fcj_zp = t1.fcj_zp
            })
            .ToListAsync();
    }
    public async Task<List<MixSwitchSetDto>> GetMixSwitchSetAsync(int pid)
    {
        var lisDb = _dbFactory("LIS");
        return await lisDb.Queryable<BASEGROUP,GROUP>( (t1, t2) => t1.PID == pid )
            .Where((t1, t2) => t1.ID.ToString() == t2.Code)
            .Select((t1, t2) => new MixSwitchSetDto
            {
                id = t2.ID,
                bname = t2.BName,
                attributes = t2.ATTRIBUTES,
            })
            .ToListAsync();
    }

    public async Task<bool> updateMixSwitchSetAsync(MixSwitchSetDto switchSetDto)
    {
        var lisDb = _dbFactory("LIS");
            var result = await lisDb.Updateable<GROUP>()
                .SetColumns(it => new GROUP()
                {
                    ATTRIBUTES = switchSetDto.attributes
                })
                .Where(it => it.ID == switchSetDto.id)
                .ExecuteCommandAsync();
            return result > 0;
    }
}
