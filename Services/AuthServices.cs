using webapi.Models.BaseData;
using webapi.Repository;

namespace webapi.Services;

public class AuthServices : IAuthServices
{
    private readonly IBaseRepository<UserToken> _repo;

    public AuthServices(IBaseRepository<UserToken> repo)
    {
        _repo = repo;
    }

    public List<UserToken> GetAll() => _repo.GetAll();

    public int Add(UserToken entity) => _repo.Add(entity);
    public int Update(UserToken entity) => _repo.Update(entity);
}
