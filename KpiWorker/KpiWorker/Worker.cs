using KpiWorker.Data;
using KpiWorker.Models;
using Microsoft.EntityFrameworkCore;

namespace KpiWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                        var totalAccounts = await db.Accounts.CountAsync(stoppingToken);
                        var activeAccounts = await db.Accounts.CountAsync(a => a.Status == "Active", stoppingToken);
                        var totalBalance = await db.Accounts.SumAsync(a => a.Balance, stoppingToken);
                        var avgBalance = totalAccounts > 0 ? totalBalance / totalAccounts : 0;

                        // Prevent duplicate insert (idempotency)
                        var today = DateTime.UtcNow.Date;
                        var existing = await db.KpiSnapshots.FirstOrDefaultAsync(k => k.SnapshotDate == today, stoppingToken);

                        if (existing == null)
                        {
                            var snapshot = new KpiSnapshot
                            {
                                SnapshotDate = today,
                                TotalAccounts = totalAccounts,
                                ActiveAccounts = activeAccounts,
                                TotalBalance = totalBalance,
                                AverageBalance = avgBalance
                            };

                            db.KpiSnapshots.Add(snapshot);
                            await db.SaveChangesAsync(stoppingToken);

                            _logger.LogInformation("KPI Snapshot saved at: {time}", DateTimeOffset.Now);
                        }
                        else
                        {
                            _logger.LogInformation("KPI Snapshot already exists for {date}", today);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while calculating KPIs.");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // run every 1 min
            }
        }
    }
}
