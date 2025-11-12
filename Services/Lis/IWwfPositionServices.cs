using webapi.Dtos;
using webapi.Models.LIS;
namespace webapi.Services.Lis;

public interface IWwfPositionServices
{
    public List<WWF_POSITION> GetAll();
}