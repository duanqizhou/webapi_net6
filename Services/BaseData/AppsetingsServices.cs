using System.Linq.Expressions;
using webapi.Models.BaseData;
using webapi.Repository;

namespace webapi.Services
{
    public class AppsetingsServices : IAppsetingsServices
    {
        private readonly IBaseRepository<APPSETTINGS> _repo;
        public AppsetingsServices(IBaseRepository<APPSETTINGS> repo)
        {
            _repo = repo;
        }
        public List<APPSETTINGS> GetAll() => _repo.GetAll();

        public List<APPSETTINGS> GetList(Expression<Func<APPSETTINGS, bool>> predicate)
        {
            return _repo.GetList(predicate);
        }
    }
}