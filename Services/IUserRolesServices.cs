using webapi.Models;

namespace webapi.Services;

public interface IUserRolesServices : IBaseServices<UserRoles>
{
    Task<List<UserRoles>> GetUserIDAsync(int userId);
    Task<List<string>> GetRoleNamesByUserIdAsync(int userId);

}
