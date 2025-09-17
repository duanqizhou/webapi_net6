using webapi.Models;

namespace webapi.Services;

public interface IBaseDrugServices : IBaseServices<DM_DICT>
{
    Task<(List<DM_DICT> List, int Total)> GetDM_DICTAsyncTotal(DictLibListDto dto);
    Task<int> GetMaxCode();
}
