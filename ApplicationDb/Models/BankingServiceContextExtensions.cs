using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EntityModels;
public static class BankingServiceContextExtensions
{
    /// <summary>
    /// Метод расширения для добавления контекста базы данных в
    /// коллекцию сервисов зависимостей, применяя IServiceCollection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <returns> ServiceCollection для добавления других сервисов </returns>
    public static IServiceCollection AddBankingServiceContext(
        this IServiceCollection services, string connectionString =
        "Host=localhost;Port=5432;Database=BankingService;" +
        "Username=postgres;Password=sqlserver")
    {
        //Строка подключения для API сервиса вынесена
        //в appsettings.json файл проекта BankAPI
        services.AddDbContext<BankingServiceContext>(options =>
        options.UseNpgsql(connectionString));
        return services;
    }
}