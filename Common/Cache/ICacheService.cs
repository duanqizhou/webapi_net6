using webapi.Models.BaseData;
using System.Collections.Generic;
using webapi.Models.LIS;
using webapi.Dtos.His;

namespace webapi.Common
{
    public interface ICacheService
    {
        List<DEPARTMENT> GetDepartments();
        List<EMPLOYEE> GetEmployees();
        List<APPSETTINGS> GetAppsettings();

        List<WWF_DEPT> GetWwfDept();
        List<WwfPersonDto> GetWwfPerson();
        List<WWF_SYS> GetWwfSys();

        void UpdateCache<T>(string cacheName, List<T> cacheList);
        void RemoveCache(string cacheName);
    }
}
