using webapi.Dtos;
using webapi.Models.LIS;
namespace webapi.Services.Lis;

public interface IWwfOrgFuncServices
{
    public List<WWF_ORG_FUNC> GetAll();
}