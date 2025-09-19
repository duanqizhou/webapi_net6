using webapi.Models.BaseData;
using webapi.Repository;

namespace webapi.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IBaseRepository<EMPLOYEE> _repo;
        public EmployeeServices(IBaseRepository<EMPLOYEE> repo)
        {
            _repo = repo;
        }

        public int Add(EMPLOYEE entity) => _repo.Add(entity);

        public List<EMPLOYEE> GetAll() => _repo.GetAll();

        public EMPLOYEE GetById(int id) => _repo.GetById(id);
    }
}
