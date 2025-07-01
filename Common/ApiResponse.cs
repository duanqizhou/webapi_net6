namespace webapi.Common;

public class ApiResponse
{
    public int Code { get; set; }
    public string Message { get; set; } = "";
    public object? Data { get; set; }

    public static ApiResponse Ok(object? data = null) => new() { Code = 0, Message = "success", Data = data };

    public static ApiResponse Error(string msg, int code = 500) => new() { Code = code, Message = msg };
}
