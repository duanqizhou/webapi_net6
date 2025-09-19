using webapi.Models.BaseData;
using webapi.Dtos;
namespace webapi.Services;

public interface IBaseMatServices : IBaseServices<MedicalMaterial>
{
    Task<(List<MedicalMaterial> List, int Total)> GetMatAsyncTotal(MatLibListDto dto);
}
