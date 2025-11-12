using webapi.Dtos.Lis;
using webapi.Models.LIS;
namespace webapi.Services.Lis;

public interface IWwfOrgServices
{
    Task<List<WwfOrgDto>> GetOrgWithDeptAndPositionAsync(string personId);
}