using KpiWorker;
using KpiWorker.Data;
using Microsoft.EntityFrameworkCore;


var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync(); ;
