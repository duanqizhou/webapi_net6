using webapi.Models;
using webapi.Repository;

namespace webapi.Services
{
    public class DeptServices : IDeptServices
    {
        private readonly IBaseRepository<DEPARTMENT> _repo;
        public DeptServices(IBaseRepository<DEPARTMENT> repo)
        {
            _repo = repo;
        }
        public List<DEPARTMENT> GetAll() => _repo.GetAll();
    }
}
