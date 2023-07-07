using EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace ServicesDb;

public class ClientService : IClientService
{
	//потокобезопасный словрь для хранения клиентов 
    private static ConcurrentDictionary<Guid, Client>? clientCache;

	private BankingServiceContext db;
	public ClientService(BankingServiceContext injectedContext)
	{
		db = injectedContext;
		if (clientCache is null)
		{
			clientCache = new ConcurrentDictionary<Guid, Client>(
			db.Clients.Include(p=>p.Accounts).ToDictionary(c => c.ClientId));
		}
	}

    /// <summary>
    /// Метод добавления клиента в БД
    /// </summary>
    /// <param name="c">Клиент</param>
    /// <returns>Клиент, если процесс добавления прошёл успешно</returns>
    public async Task<Client?> AddClientAsync(Client c)
	{
		EntityEntry<Client> added = await db.Clients.AddAsync(c);
		int affected = await db.SaveChangesAsync();
		if (affected == 1)
		{
			if (clientCache is null)
				return c;
			await db.AddAsync(new Account(c.ClientId, "RUB", 0));
			await db.SaveChangesAsync();
			//нового клиента в кэш, иначе вызываем UpdateCache
			return clientCache.AddOrUpdate(c.ClientId, c, UpdateCache);
		}
			return null;
	}

	/// <summary>
	/// Метод добавления лицевого счёта в БД
	/// </summary>
	/// <param name="account">Лицевой счёт</param>
	/// <returns>Состояние добавленного аккаунта</returns>
    public Task<int> AddAccount(Account account)
    {
        EntityEntry<Account> added = db.Accounts.Add(account);
        return Task.FromResult(db.SaveChanges());
    }

	/// <summary>
	/// Метод получения всех клиентов из кэша
	/// </summary>
	/// <returns>Содержимое кэша</returns>
    public Task<IEnumerable<Client>> RetrieveAllAsync() => Task.FromResult(clientCache is null ?
		Enumerable.Empty<Client>() : clientCache.Values);

	/// <summary>
	/// Метод фильтрации клиентов по дате рождения
	/// </summary>
	/// <param name="startDate"></param>
	/// <param name="endDate"></param>
	/// <returns>Отфильтрованная коллекция</returns>
	public Task<IEnumerable<Client>> GetFiltered(DateOnly startDate, DateOnly endDate) => 
		Task.FromResult(clientCache is null ? Enumerable.Empty<Client>() : clientCache.Values.Where(p =>
		p.DateOfBirth > startDate && p.DateOfBirth < endDate).OrderBy(p => p.DateOfBirth));
	
	/// <summary>
	/// Метод получения клиента по идентификатору
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Клиент из кэша</returns>
	public Task<Client?> RetrieveClientAsync(Guid id)
	{
		if (clientCache is null) return null!;
		clientCache.TryGetValue(id, out Client? client);
		return Task.FromResult(client);
	}

	/// <summary>
	/// Метод обновления кэша
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <param name="c">Клиент</param>
	/// <returns>Клиент, если кэш обновлён</returns>
	private Client UpdateCache(Guid id, Client c)
	{
		Client? old;
		if (clientCache is not null)
		{
			if (clientCache.TryGetValue(id, out old))
			{
				if (clientCache.TryUpdate(id, c, old))
				{
					return c;
				}
			}
		}
		return null!;
	}

	/// <summary>
	/// Метод обновления данных клиента
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <param name="c">Клиент</param>
	/// <returns>Клиент, если успешно обновлён</returns>
	public async Task<Client?> UpdateClientAsync(Guid id, Client c)
	{
        db.Entry(c).State = EntityState.Detached;
        db.Entry(c).State = EntityState.Modified;
        int affected = await db.SaveChangesAsync();
		if (affected == 1)
		{
			return UpdateCache(id, c);
		}
		return null;
	}

	/// <summary>
	/// Метод удаления клиента по идентификатору
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Статус операции</returns>
    public async Task<bool?> DeleteClientAsync(Guid id)
	{
		Client? c = db.Clients.Find(id);
		if (c is null) return null;
		db.Clients.Remove(c);
		int affected = await db.SaveChangesAsync();
		if (affected == 1)
		{
			if (clientCache is null) return null;
			return clientCache.TryRemove(id, out c);
		}
		else
		{
			return null;
		}
	}

	/// <summary>
	/// Метод удаления лицевого счёта по идентификатору
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Статус операции</returns>
    public async Task<bool?> DeleteAccountAsync(Guid id)
    {
        Account? c = db.Accounts.Find(id);
        if (c is null) return null;
        db.Accounts.Remove(c);
        int affected = await db.SaveChangesAsync();
        if (affected == 1)
        {
            clientCache = new (db.Clients.Include(p => p.Accounts).ToDictionary(c => c.ClientId));
            return true;
        }
        else
        {
            return null;
        }
    }
}