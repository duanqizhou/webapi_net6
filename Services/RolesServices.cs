using webapi.Models;
using webapi.Repository;

namespace webapi.Services;

public class RolesServices : IRolesServices
{
    private readonly IBaseRepository<Roles> _repo;

    public RolesServices(IBaseRepository<Roles> repo)
    {
        _repo = repo;
    }

    public List<Roles> GetAll() => _repo.GetAll();

    public int Add(Roles entity) => _repo.Add(entity);
    public int Update(Roles entity) => _repo.Update(entity);
    public int Delete(int id) => _repo.Delete(id);
    public Roles GetById(int id) => _repo.GetById(id);
}
