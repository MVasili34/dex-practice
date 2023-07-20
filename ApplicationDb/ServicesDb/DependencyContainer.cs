using EntityModels;
using Microsoft.Extensions.DependencyInjection;

namespace ServicesDb;

public class DependencyContainer
{
    /// <summary>
    /// Метод конфигурации контейнера внедрения зависимостей
    /// для проведения Unit-тестирования
    /// </summary>
    /// <returns>IServiceProvider</returns>
    public static IServiceProvider Configure() => new ServiceCollection()
        .AddTransient<IClientService, ClientService>()
        .AddTransient<IEmployeeService, EmployeeService>()
        .AddBankingServiceContext()
        .BuildServiceProvider();
}
