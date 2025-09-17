using webapi.Models;

namespace webapi.Services;

public interface IUserServices : IBaseServices<ACCOUNTS>
{
    Task<(List<ACCOUNTS> List, int Total)> GetUsersAsyncTotal(UserQueryDto dto);
    //获取当前最大的登录ID
    Task<int> GetMaxLoginIdAsync();
}
