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
        /// 执行事务操作，要么全部成功，要么全部失败
        /// </summary>
        public async Task<ApiResponse> ExecuteAsync(Func<ISqlSugarClient, Task> action)
        {
            try
            {
                var scope = _db as SqlSugarScope;
                if (scope == null)
                    throw new InvalidCastException("请确保 ISqlSugarClient 实际为 SqlSugarScope 类型");

                var result = await scope.UseTranAsync(async () =>
                {
                    await action(_db);
                });

                return result.IsSuccess ? ApiResponse.Ok() : ApiResponse.Error(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error("事务异常: " + ex.Message);
            }
        }

    }
}
