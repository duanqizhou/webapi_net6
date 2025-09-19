using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SqlSugar;
using webapi.Models.BaseData;

public class PermissionFilter : IAsyncActionFilter
{
    private readonly ISqlSugarClient _db;

    public PermissionFilter(ISqlSugarClient db)
    {
        _db = db;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
         var endpoint = context.HttpContext.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
        {
            await next();
            return;
        }
        var userIdStr = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdStr, out var userId))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var path = context.HttpContext.Request.Path.Value;
        var routePattern = "/" + (endpoint as Microsoft.AspNetCore.Routing.RouteEndpoint)?.RoutePattern?.RawText;
        var method = context.HttpContext.Request.Method;

        var hasPermission = await _db.Queryable<Permissions>() 
            .Where(p=>p.Url == routePattern)
            .AnyAsync();
        Console.WriteLine("hasPermission��" + hasPermission.ToString());

        if (!hasPermission)
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }
}
