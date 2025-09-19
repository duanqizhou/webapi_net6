using System.Linq.Expressions;
using webapi.Models.BaseData;

namespace webapi.Services;

public interface IEmployeeServices
{
    List<EMPLOYEE> GetAll();
    int Add(EMPLOYEE entity);
    EMPLOYEE GetById(int id);

}
