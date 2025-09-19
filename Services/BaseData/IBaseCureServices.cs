using webapi.Models.BaseData;
using webapi.Dtos;
namespace webapi.Services;

public interface IBaseCureServices : IBaseServices<Consulting>
{
    Task<(List<Consulting> List, int Total)> GetCureAsyncTotal(CureLibListDto dto);
}
