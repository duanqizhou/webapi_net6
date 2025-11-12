using webapi.Dtos;
using webapi.Models.LIS;
namespace webapi.Services.Lis;

public interface IWwfSysServices
{
    public List<WWF_SYS> GetAll();
}