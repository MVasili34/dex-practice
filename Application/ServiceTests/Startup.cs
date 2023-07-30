using Microsoft.Extensions.DependencyInjection;
using Services;
using Models;
using Services.Storage;

namespace ServiceTests.Tests;

internal class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IClientStorage, ClientService>();
        services.AddTransient<IEmployeeStorage, EmployeeService>();
    }

    /// <summary>
    /// Метод конфигурации контейнера внедрения зависимостей клиентов
    /// с влорженными данными для проведения Unit-тестирования
    /// </summary>
    /// <returns>IServiceProvider</returns>
    public static IClientStorage ConfigureClients(Dictionary<Client, List<Account>> data)
    {
        ServiceCollection services = new();

        services.AddTransient<IClientStorage>(sp => new ClientService(data));

        return services.BuildServiceProvider().GetService<IClientStorage>()!;
    }

    /// <summary>
    /// Метод конфигурации контейнера внедрения зависимостей сотрудников
    /// с влорженными данными для проведения Unit-тестирования
    /// </summary>
    /// <returns>IServiceProvider</returns>
    public static IEmployeeStorage ConfigureEmployees(List<Employee> data)
    {
        ServiceCollection services = new();

        services.AddTransient<IEmployeeStorage>(sp => new EmployeeService(data));

        return services.BuildServiceProvider().GetService<IEmployeeStorage>()!;
    }
}
