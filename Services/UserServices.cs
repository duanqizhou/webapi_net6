using webapi.Models;
using webapi.Repository;

namespace webapi.Services;

public class UserServices : IUserServices
{
    private readonly IBaseRepository<Users> _repo;

    public UserServices(IBaseRepository<Users> repo)
    {
        _repo = repo;
    }

    public List<Users> GetAll() => _repo.GetAll();

    public int Add(Users entity) => _repo.Add(entity);
    public int Update(Users entity) => _repo.Update(entity);
    public int Delete(int id) => _repo.Delete(id);
    public Users GetById(int id) => _repo.GetById(id);
}
