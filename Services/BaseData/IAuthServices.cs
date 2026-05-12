using webapi.Models.BaseData;

namespace webapi.Services;

public interface IAuthServices
{
    List<UserToken> GetAll();
    UserToken? GetByRefreshToken(string refreshToken);
    int Add(UserToken entity);
    int Update(UserToken entity);
}
