using webapi.Models;

namespace webapi.Repository;

public interface IBaseRepository<T>
{
    List<T> GetAll();
    int Add(T entity);
    int Update(T entity);
    int Delete(int id);
    T GetById(int id);
}
