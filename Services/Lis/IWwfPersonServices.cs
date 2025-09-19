using webapi.Dtos;
using webapi.Models.LIS;

namespace webapi.Services.Lis;

public interface IWwfPersonServices
{
    Task<WWF_PERSON> BllUserPassOk(string fperson_id, string fpass);
    public List<WWF_PERSON> GetAll(string dbName);
    
}