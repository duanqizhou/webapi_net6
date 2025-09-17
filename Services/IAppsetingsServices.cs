using System.Linq.Expressions;
using webapi.Models;

namespace webapi.Services;

public interface IAppsetingsServices
{
    List<APPSETTINGS> GetAll();
    List<APPSETTINGS> GetList(Expression<Func<APPSETTINGS, bool>> predicate);

}


