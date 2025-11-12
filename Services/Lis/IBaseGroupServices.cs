using webapi.Dtos;
using webapi.Models.LIS;
namespace webapi.Services.Lis;

public interface IBaseGroupServices
{
    public List<BASEGROUP> GetAll();
}