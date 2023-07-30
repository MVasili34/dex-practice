using EntityModels;
using Microsoft.Extensions.DependencyInjection;
using ServicesDb;

namespace ServicesDbTests.Tests;

internal class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IClientService, ClientService>();
        services.AddTransient<IEmployeeService, EmployeeService>();
        services.AddBankingServiceContext();
    }
}
