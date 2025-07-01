using webapi.Models;

namespace webapi.Services;

public interface IAuthServices
{
    List<UserToken> GetAll();
    int Add(UserToken entity);
    int Update(UserToken entity);
}
