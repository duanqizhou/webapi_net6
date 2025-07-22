using SqlSugar;
using webapi.Models;
using webapi.Repository;

namespace webapi.Services;

public class UserRolesServices : IUserRolesServices
{
    private readonly IBaseRepository<UserRoles> _repo;
    private readonly ISqlSugarClient _db;

    public UserRolesServices(IBaseRepository<UserRoles> repo, ISqlSugarClient db)
    {
        _repo = repo;
        _db = db;
    }

    public List<UserRoles> GetAll() => _repo.GetAll();

    public int Add(UserRoles entity) => _repo.Add(entity);
    public int Update(UserRoles entity) => _repo.Update(entity);
    public int Delete(int id) => _repo.Delete(id);
    public UserRoles GetById(int id) => _repo.GetById(id);
    public async Task<List<UserRoles>> GetUserIDAsync(int id) => await _db.Queryable<UserRoles>().Where(ur => ur.UserId == id).ToListAsync();
    public async Task<List<string>> GetRoleNamesByUserIdAsync(int userId)
         => await _db.Queryable<UserRoles, Roles>((a, b) => new JoinQueryInfos(
                                JoinType.Left, a.RoleId == b.Id))
                              .Where((a, b) => a.UserId == userId)
                              .Select((a, b) => b.Name)
                              .ToListAsync();
    

}
