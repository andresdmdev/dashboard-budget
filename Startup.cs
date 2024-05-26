using ApplicationServices.UserServices;
using InfraestructureDB.Context;
using InfraestructureDB.Repository;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddLogging();

        string connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");

        services.AddDbContext<DashboardDBContext>(options =>
            options.UseSqlServer(connection)
        );

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IOperationRepository, OperationRepository>();
        services.AddScoped<IOperationService, OperationService>();
    })
    .Build();

host.Run();
