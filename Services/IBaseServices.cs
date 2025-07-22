using webapi.Models;

namespace webapi.Services;

public interface IBaseServices<T> where T : class
{
    List<T> GetAll();
    int Add(T entity);
    int Update(T entity);
    int Delete(int id);
    T GetById(int id);
}
