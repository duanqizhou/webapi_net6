using Microsoft.Extensions.Caching.Memory;
using webapi.Models.BaseData;
using System.Collections.Generic;
using System;
using webapi.Services;

namespace webapi.Common
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDeptServices _deptServices;
        private readonly IEmployeeServices _employeeServices;
        private readonly IAppsetingsServices _appsetingsServices;

        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(CacheService));

        public CacheService(IMemoryCache memoryCache, IDeptServices deptServices, IEmployeeServices employeeServices, IAppsetingsServices appsetingsServices)
        {
            _memoryCache = memoryCache;
            _deptServices = deptServices;
            _employeeServices = employeeServices;
            _appsetingsServices = appsetingsServices;   
        }

        // 手动更新缓存
        public void UpdateCache<T>(string cacheName, List<T> cacheList)
        {
            _memoryCache.Set(cacheName, cacheList);
        }

        public void RemoveCache(string cacheName)
        {
            _memoryCache.Remove(cacheName);
        }

        // 获取缓存数据的通用方法
        private List<T> GetCachedData<T>(string cacheName, Func<List<T>> dataLoader)
        {
            // 尝试从缓存中读取数据
            var cachedData = _memoryCache.Get<List<T>>(cacheName);
            if (cachedData == null)
            {
                // 缓存未命中，获取数据并存入缓存
                
                cachedData = dataLoader();
                _logger.Info($"缓存未命中，正在加载数据: {cacheName}");
                // 设置缓存数据
                _memoryCache.Set(cacheName, cachedData, CreateMemoryCacheOptions());

                // 可选：记录缓存移除事件
                Console.WriteLine($"缓存移除: {cacheName}");
            }

            return cachedData;
        }

        // 获取部门数据
        public List<DEPARTMENT> GetDepartments()
        {
            return GetCachedData(CacheName.department, LoadDepartmentsFromDatabase);
        }

        // 从数据库加载部门数据
        private List<DEPARTMENT> LoadDepartmentsFromDatabase()
        {
            return _deptServices.GetAll() ?? new List<DEPARTMENT>();
        }

        // 获取员工数据
        public List<EMPLOYEE> GetEmployees()
        {
            return GetCachedData(CacheName.employee, LoadEmployeesFromDatabase);
        }

        // 从数据库加载员工数据
        private List<EMPLOYEE> LoadEmployeesFromDatabase()
        {
            return _employeeServices.GetAll() ?? new List<EMPLOYEE>();
        }

        public List<APPSETTINGS> GetAppsettings()
        {
            return GetCachedData(CacheName.appsetings, LoadAppsetingsFromDatabase);
        }

        private List<APPSETTINGS> LoadAppsetingsFromDatabase()
        {
            return _appsetingsServices.GetAll() ?? new List<APPSETTINGS>();
        }



        // 创建 MemoryCacheEntryOptions
        private MemoryCacheEntryOptions CreateMemoryCacheOptions()
        {

            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                SlidingExpiration = TimeSpan.FromMinutes(30),
                Priority = CacheItemPriority.Normal
            };
            options.PostEvictionCallbacks.Add(new PostEvictionCallbackRegistration
            {
                EvictionCallback = (key, value, reason, state) =>
                {
                    Console.WriteLine($"缓存移除: {key}, 原因: {reason}");
                }
            });

            return options;
        }
    }

    public class PostEvictionCallback
    {
        public Action<object, object, EvictionReason, object> EvictionCallback { get; set; }
    }
}
