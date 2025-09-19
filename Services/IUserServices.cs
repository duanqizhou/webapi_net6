using webapi.Models.BaseData;
using webapi.Dtos;

namespace webapi.Services;

public interface IUserServices : IBaseServices<ACCOUNTS>
{
    Task<(List<ACCOUNTS> List, int Total)> GetUsersAsyncTotal(UserQueryDto dto);
    //获取当前用户的登录ID
    Task<int> GetMaxLoginIdAsync();
}
