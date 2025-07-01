using webapi.Models;

namespace webapi.Services;

public interface IUserServices
{
    List<Users> GetAll();
    int Add(Users entity);
    int Update(Users entity);
    int Delete(int id);
    Users GetById(int id);
}
