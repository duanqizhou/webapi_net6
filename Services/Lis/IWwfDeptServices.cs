using webapi.Dtos;
using webapi.Models.LIS;
namespace webapi.Services.Lis;

public interface IWwfDeptServices
{
    public List<WWF_DEPT> GetAll();
}