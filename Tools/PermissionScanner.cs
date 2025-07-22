using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using webapi.Models;
using System.ComponentModel;

namespace webapi.Tools
{
    public static class PermissionScanner
    {
        public static void GeneratePermissions(ISqlSugarClient db)
        {
            var permissions = new List<Permissions>();

            var controllerTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var controller in controllerTypes)
            {
                string controllerName = controller.Name.Replace("Controller", "").ToLower();
                string routePrefix = controller.GetCustomAttribute<RouteAttribute>()?.Template ?? $"api/{controllerName}";
                routePrefix = routePrefix.Replace("[controller]", controllerName).Trim('/');

                var methods = controller.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                foreach (var method in methods)
                {
                    var httpMethodAttr = method.GetCustomAttributes()
                        .FirstOrDefault(attr => attr is HttpGetAttribute || attr is HttpPostAttribute || attr is HttpPutAttribute || attr is HttpDeleteAttribute);

                    if (httpMethodAttr == null) continue;

                    string url = "";
                    string httpMethod = "";

                    if (httpMethodAttr is HttpGetAttribute getAttr)
                    {
                        url = getAttr.Template ?? "";
                        httpMethod = "GET";
                    }
                    else if (httpMethodAttr is HttpPostAttribute postAttr)
                    {
                        url = postAttr.Template ?? "";
                        httpMethod = "POST";
                    }
                    else if (httpMethodAttr is HttpPutAttribute putAttr)
                    {
                        url = putAttr.Template ?? "";
                        httpMethod = "PUT";
                    }
                    else if (httpMethodAttr is HttpDeleteAttribute deleteAttr)
                    {
                        url = deleteAttr.Template ?? "";
                        httpMethod = "DELETE";
                    }

                    // 拼接完整路径
                    string fullUrl = CombineUrls("", routePrefix, url);

                    // 获取描述作为权限名（如果有）
                    string name = method.GetCustomAttribute<DescriptionAttribute>()?.Description ?? method.Name;

                    permissions.Add(new Permissions
                    {
                        Name = name,
                        Url = "/" + fullUrl,
                        Method = httpMethod
                    });
                }
            }

            // 清除旧的
            db.Deleteable<Permissions>().ExecuteCommand();

            // 去重插入
            var distinctList = permissions
                .GroupBy(p => new { p.Url, p.Method })
                .Select(g => g.First())
                .ToList();

            db.Insertable(distinctList).ExecuteCommand();
        }

        private static string CombineUrls(params string[] parts)
        {
            return string.Join("/", parts
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select(p => p.Trim('/'))
            );
        }
    }
}
