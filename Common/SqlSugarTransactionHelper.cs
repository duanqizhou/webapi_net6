using SqlSugar;
using System;
using System.Threading.Tasks;

namespace webapi.Common
{
    public class SqlSugarTransactionHelper
    {
        private readonly ISqlSugarClient _db;

        public SqlSugarTransactionHelper(ISqlSugarClient db)
        {
            _db = db;
        }

        /// <summary>
        /// 默认库事务
        /// </summary>
        public async Task<ApiResponse> ExecuteAsync(Func<ISqlSugarClient, Task> action)
        {
            try
            {
                var scope = _db as SqlSugarScope
                            ?? throw new InvalidCastException("请确保 ISqlSugarClient 实际为 SqlSugarScope 类型");

                var result = await scope.UseTranAsync(async () =>
                {
                    await action(scope); // 默认库
                });

                return result.IsSuccess ? ApiResponse.Ok() : ApiResponse.Error(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error("事务异常: " + ex.Message);
            }
        }

        /// <summary>
        /// 指定库事务（通过库 ID 获取连接）
        /// </summary>
// <summary>
        /// 执行事务操作（要么全部成功，要么全部失败）
        /// 默认走 SqlSugarScope.UseTranAsync，全局事务
        /// </summary>
        public async Task<ApiResponse> ExecuteAsync(Func<ISqlSugarClient, Task> action, string? dbId = null)
        {
            try
            {
                var scope = _db as SqlSugarScope 
                    ?? throw new InvalidCastException("ISqlSugarClient 必须是 SqlSugarScope 类型");

                if (string.IsNullOrEmpty(dbId))
                {
                    // ✅ 全局事务（可支持跨库）
                    var result = await scope.UseTranAsync(async () =>
                    {
                        await action(scope);
                    });
                    return result.IsSuccess ? ApiResponse.Ok() : ApiResponse.Error(result.ErrorMessage);
                }
                else
                {
                    // ✅ 指定某个库（单库事务）
                    var provider = scope.GetConnectionScope(dbId);
                    provider.Ado.BeginTran();
                    try
                    {
                        await action(provider);
                        provider.Ado.CommitTran();
                        return ApiResponse.Ok();
                    }
                    catch (Exception ex)
                    {
                        provider.Ado.RollbackTran();
                        return ApiResponse.Error("事务异常：" + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                return ApiResponse.Error("事务异常: " + ex.Message);
            }
        }

        /// <summary>
        /// 跨库事务（同时操作多个库）
        /// </summary>
        public async Task<ApiResponse> ExecuteMultiDbAsync(Func<SqlSugarScope, Task> action)
        {
            try
            {
                var scope = _db as SqlSugarScope
                            ?? throw new InvalidCastException("请确保 ISqlSugarClient 实际为 SqlSugarScope 类型");

                scope.BeginTran();
                try
                {
                    await action(scope);
                    scope.CommitTran();
                    return ApiResponse.Ok();
                }
                catch (Exception ex)
                {
                    scope.RollbackTran();
                    return ApiResponse.Error("跨库事务失败: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return ApiResponse.Error("跨库事务异常: " + ex.Message);
            }
        }
    }
}

/*
默认库（BaseData）
await _tranHelper.ExecuteAsync(async db =>
{
    await db.Insertable(new DM_DICT { CODE = "003", CNAME = "默认库测试" }).ExecuteCommandAsync();
});

指定库（LIS）
await _tranHelper.ExecuteAsync("LIS", async db =>
{
    await db.Ado.ExecuteCommandAsync("UPDATE SomeLisTable SET Status=1 WHERE Id=1001");
});

跨库事务
await _tranHelper.ExecuteMultiDbAsync(async scope =>
{
    // Default 库
    await scope.GetConnectionScope("BaseData")
               .Insertable(new DM_DICT { CODE = "004", CNAME = "BaseData测试" })
               .ExecuteCommandAsync();

    // LIS 库
    await scope.GetConnectionScope("LIS")
               .Ado.ExecuteCommandAsync("INSERT INTO Logs(Message) VALUES('跨库操作成功')");
});

*/