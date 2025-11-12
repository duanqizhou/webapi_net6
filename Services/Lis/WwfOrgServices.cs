using SqlSugar;
using webapi.Dtos.Lis;
using webapi.Models.LIS;
using webapi.Repository;
using webapi.Services.Lis;

namespace webapi.Services;

public class WwfOrgServices : IWwfOrgServices
{
    private readonly Func<string, ISqlSugarClient> _dbFactory;
    public WwfOrgServices(Func<string, ISqlSugarClient> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<WwfOrgDto>> GetOrgWithDeptAndPositionAsync(string personId)
    {
        var lisDb = _dbFactory("LIS");

        return await lisDb.Queryable<WWF_ORG, WWF_DEPT, WWF_POSITION>(
                (t, d, p) => new JoinQueryInfos(
                    JoinType.Left, t.fdept_id == d.fdept_id,
                    JoinType.Left, t.fposition_id == p.fposition_id
                )
            )
            .Where((t, d, p) => t.fperson_id == personId && t.ftype == "person")
            .Select((t, d, p) => new WwfOrgDto
            {
                fperson_id = t.fperson_id,
                fdept_id = t.fdept_id,
                fposition_id = t.fposition_id,
                ftype = t.ftype,
                fdept_name = d.fname,
                fposition_name = p.fname
            })
            .ToListAsync();
    }
}
