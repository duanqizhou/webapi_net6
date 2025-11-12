using webapi.Dtos;
using webapi.Dtos.Lis;
using webapi.Models.BaseData;
using webapi.Models.LIS;

namespace webapi.Services
{
    public interface ISamSampleItemServices
    {
        Task<(List<SAM_SAMPLE_ITEM> List, int Total)> GetSamSampleItemPageAsyncTotal(SamSampleItemPageDto dto);

    }
}