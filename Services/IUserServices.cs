using webapi.Models;

namespace webapi.Services;

public interface IUserServices : IBaseServices<ACCOUNTS>
{
    Task<(List<ACCOUNTS> List, int Total)> GetUsersAsyncTotal(UserQueryDto dto);
    //��ȡ��ǰ���ĵ�¼ID
    Task<int> GetMaxLoginIdAsync();
}
