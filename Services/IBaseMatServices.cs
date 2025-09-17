using webapi.Models;

namespace webapi.Services;

public interface IBaseMatServices : IBaseServices<MedicalMaterial>
{
    Task<(List<MedicalMaterial> List, int Total)> GetMatAsyncTotal(MatLibDto dto);
}
