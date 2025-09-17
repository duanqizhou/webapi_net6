using webapi.Models;

namespace webapi.Services;

public interface IBaseCureServices : IBaseServices<Consulting>
{
    Task<(List<Consulting> List, int Total)> GetCureAsyncTotal(CureLibDto dto);
}
