using SqlSugar;
using System;
using System.Threading.Tasks;

namespace webapi.Common
{
    public class SqlSugarTransactionHelper
    {
        private readonly SqlSugarScope _scope;

        public SqlSugarTransactionHelper(SqlSugarScope scope)
        {
            _scope = scope ?? throw new ArgumentNullException(nameof(scope));
        }

        /// <summary>
        /// 默认库事务（全局事务或单库事务）
        /// </summary>
        public async Task<ApiResponse> ExecuteAsync(Func<ISqlSugarClient, Task> action, string? dbId = null)
        {
            try
            {
                if (string.IsNullOrEmpty(dbId))
                {
                    // ✅ 全局事务（可跨库）
                    var result = await _scope.UseTranAsync(async () =>
                    {
                        await action(_scope);
                    });
                    return result.IsSuccess ? ApiResponse.Ok() : ApiResponse.Error(result.ErrorMessage);
                }
                else
                {
                    // ✅ 单库事务
                    var provider = _scope.GetConnectionScope(dbId);
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
        /// 跨库事务
        /// </summary>
        public async Task<ApiResponse> ExecuteMultiDbAsync(Func<SqlSugarScope, Task> action)
        {
            try
            {
                _scope.BeginTran();
                try
                {
                    await action(_scope);
                    _scope.CommitTran();
                    return ApiResponse.Ok();
                }
                catch (Exception ex)
                {
                    _scope.RollbackTran();
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
// 默认库事务（BaseData）
await _tranHelper.ExecuteAsync(async db =>
{
    await db.Insertable(new DM_DICT { CODE = "003", CNAME = "默认库测试" }).ExecuteCommandAsync();
});

// 指定库事务（LIS）
await _tranHelper.ExecuteAsync(async db =>
{
    await db.Ado.ExecuteCommandAsync("UPDATE SomeLisTable SET Status=1 WHERE Id=1001");
}, "LIS");

// 跨库事务
await _tranHelper.ExecuteMultiDbAsync(async scope =>
{
    await scope.GetConnectionScope("BaseData")
               .Insertable(new DM_DICT { CODE = "004", CNAME = "BaseData测试" })
               .ExecuteCommandAsync();

    await scope.GetConnectionScope("LIS")
               .Ado.ExecuteCommandAsync("INSERT INTO Logs(Message) VALUES('跨库操作成功')");
});


*/