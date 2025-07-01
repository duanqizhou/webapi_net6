using log4net;
using Microsoft.Extensions.Configuration;
using SqlSugar;

namespace webapi.Services;

public class SqlSugarService
{
    public SqlSugarClient Db { get; }
    private readonly ILog log = LogManager.GetLogger(typeof(SqlSugarService));
    public SqlSugarService(IConfiguration config)
    {
        Db = new SqlSugarClient(new ConnectionConfig()
        {
            ConnectionString = config.GetConnectionString("Default"),
            DbType = DbType.SqlServer,
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute
        });

        // 可选：打印执行的 SQL 日志
        Db.Aop.OnLogExecuting = (sql, pars) =>
        {
            log.Info($" 【SQL】: {sql}");
        };
    }
}
