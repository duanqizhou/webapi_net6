using webapi.Models.BaseData;
using System.Collections.Generic;

namespace webapi.Common
{
    public interface ICacheService
    {
        List<DEPARTMENT> GetDepartments();
        List<EMPLOYEE> GetEmployees();
        List<APPSETTINGS> GetAppsettings();

        void UpdateCache<T>(string cacheName, List<T> cacheList);
        void RemoveCache(string cacheName);
    }
}
