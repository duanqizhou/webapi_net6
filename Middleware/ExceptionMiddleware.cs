using webapi.Common;

namespace webapi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); // 继续下一个中间件
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ 全局异常：");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            var result = ApiResponse.Error("服务器内部错误");
            await context.Response.WriteAsJsonAsync(result);
        }
    }
}
