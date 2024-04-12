using InfraestructureDB.Context;
using InfraestructureDB.Repository;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddLogging();
        //services.AddDbContext<DashboardDBContext>(options =>
        //{
        //    string accountEndPoint = Environment.GetEnvironmentVariable("AccountEndpoint");
        //    string accountKey = Environment.GetEnvironmentVariable("AccountKey");
        //    string dbName = Environment.GetEnvironmentVariable("DbName");

        //    options.UseCosmos(accountEndPoint, accountKey, dbName);
        //});

        string connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");

        services.AddDbContext<DashboardDBContext>(options =>
            options.UseSqlServer(connection)
        );

        services.AddScoped<IUserRepository, UserRepository>();

    })
    .Build();

host.Run();
