using SqlSugar;
using System.Linq.Expressions;
using webapi.Configs;
using webapi.Dtos;
using webapi.Dtos.Lis;
using webapi.Models.BaseData;
using webapi.Models.LIS;
using webapi.Repository;
using webapi.Services.Lis;

namespace webapi.Services;

public class SamSampleItemServices : ISamSampleItemServices
{
    private readonly IBaseRepository<SAM_SAMPLE_ITEM> _repo;
    private readonly IBaseRepository<SAM_SAMPLE_TYPE> _typeRepo;

    public SamSampleItemServices(
        IBaseRepository<SAM_SAMPLE_ITEM> repo,
        IBaseRepository<SAM_SAMPLE_TYPE> typeRepo)
    {
        _repo = repo;
        _typeRepo = typeRepo;
    }
    public async Task<(List<SAM_SAMPLE_ITEM> List, int Total)> GetSamSampleItemPageAsyncTotal(SamSampleItemPageDto dto)
    {
        RefAsync<int> total = 0;
        // 先获取分页数据
        var list = await _repo.GetPagedAsync(
            dto.CurrentPage,
            dto.Size,
            u => (string.IsNullOrEmpty(dto.SampleName) || u.SampleName.Contains(dto.SampleName))
                && (string.IsNullOrEmpty(dto.SampleCategory) || u.SampleCategory.Contains(dto.SampleCategory)),
            total,
            Db_LIS.Name
        );
        // 获取关联的类型数据
        var sampleTypeIds = list.Select(x => x.SampleTypeID).Distinct().ToList();
        var sampleTypes = _typeRepo.GetList(x => sampleTypeIds.Contains(x.fsample_type_id), Db_LIS.Name);
        var typeDict = sampleTypes.ToDictionary(x => x.fsample_type_id, x => x.fname);

        foreach (var item in list)
        {
            var sampleType = sampleTypes.FirstOrDefault(x => x.fsample_type_id == item.SampleTypeID);
            if (sampleType != null)
            {
                item.SampleTypeID = sampleType.fname;
            }
        }
        return (list, total);
    }

}
