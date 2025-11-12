using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace webapi.Jobs;

public class BackgroundJob : BackgroundService
{
    private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(BackgroundJob));
    private readonly string _logPath = Path.Combine(AppContext.BaseDirectory, "logs");

    public BackgroundJob()
    {
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // 用两个独立的循环任务同时运行
        var syncTask = RunDataSyncAsync(stoppingToken);
        var logTask = RunLogCleanupAsync(stoppingToken);

        await Task.WhenAll(syncTask, logTask);
    }

    /// <summary>
    /// 每 3 秒执行一次数据同步
    /// </summary>
    private async Task RunDataSyncAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.Info($"执行数据同步任务: {DateTime.Now}");
                // TODO: 写你的数据库同步逻辑

            }
            catch (Exception ex)
            {
                _logger.Info("数据同步任务异常");
            }

            await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
        }
    }

    /// <summary>
    /// 每天凌晨清理 15 天前日志
    /// </summary>
    private async Task RunLogCleanupAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var nextRunTime = now.Date.AddDays(1); // 第二天 0 点
            var delay = nextRunTime - now;

            await Task.Delay(delay, stoppingToken);

            try
            {
                if (Directory.Exists(_logPath))
                {
                    var files = Directory.GetFiles(_logPath, "*.log", SearchOption.AllDirectories);

                    foreach (var file in files)
                    {
                        var creationTime = File.GetCreationTime(file);
                        if (creationTime < DateTime.Now.AddDays(-15))
                        {
                            File.Delete(file);
                            _logger.Info("已清理日志文件: {file}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Info("日志清理任务异常");
            }
        }
    }
}
