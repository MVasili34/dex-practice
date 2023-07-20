using Models;
using Services.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Services;

public static class DependencyContainer
{
    /// <summary>
    /// Метод конфигурации контейнера внедрения зависимостей клиентов
    /// для проведения Unit-тестирования
    /// </summary>
    /// <returns>IServiceProvider</returns>
    public static IServiceProvider ConfigureClients(Dictionary<Client, List<Account>>? data = null)
    {
        ServiceCollection services = new();

        if (data is null)
        {
            services.AddScoped<IClientStorage, ClientService>();
        }
        else
        {
            services.AddScoped<IClientStorage>(sp => new ClientService(data));
        }
        return services.BuildServiceProvider();
    }

    /// <summary>
    /// Метод конфигурации контейнера внедрения зависимостей сотрудников
    /// для проведения Unit-тестирования
    /// </summary>
    /// <returns>IServiceProvider</returns>
    public static IServiceProvider ConfigureEmployees(List<Employee>? data = null)
    {
        ServiceCollection services = new();

        if (data is null)
        {
            services.AddScoped<IEmployeeStorage, EmployeeService>();
        }
        else
        {
            services.AddScoped<IEmployeeStorage>(sp => new EmployeeService(data));
        }
        return services.BuildServiceProvider();
    }
}
