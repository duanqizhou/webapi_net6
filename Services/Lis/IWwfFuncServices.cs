using webapi.Dtos;
using webapi.Dtos.Lis;
using webapi.Models.LIS;
namespace webapi.Services.Lis;

public interface IWwfFuncServices
{
    public List<WWF_FUNC> GetAll();
    Task<List<WwfFuncOrgDto>> GetFuncsByOrgIdAsync(string orgId);

    Task<List<MixSwitchSetDto>> GetMixSwitchSetAsync(int pid);

    Task<bool> updateMixSwitchSetAsync(MixSwitchSetDto dto);


}