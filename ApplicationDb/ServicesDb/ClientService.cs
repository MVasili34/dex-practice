using EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace ServicesDb;

public class ClientService : IClientService
{
    private static ConcurrentDictionary<Guid, Client>? clientCache;

	private BankingServiceContext db;
	public ClientService(BankingServiceContext injectedContext)
	{
		db = injectedContext;
		if (clientCache is null)
		{
			clientCache = new ConcurrentDictionary<Guid, Client>(
			db.Clients.ToDictionary(c => c.ClientId));
		}
	}

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

	public Task<int> AddAccount(Account account)
	{
		EntityEntry<Account> added = db.Accounts.Add(account);
		return Task.FromResult(db.SaveChanges());
	}

	public Task<IEnumerable<Client>> RetrieveAllAsync() => Task.FromResult(clientCache is null ?
		Enumerable.Empty<Client>() : clientCache.Values);

	public Task<IEnumerable<Client>> GetFiltered(DateOnly startDate, DateOnly endDate) => 
		Task.FromResult(clientCache is null ? Enumerable.Empty<Client>() : clientCache.Values.Where(p =>
		p.DateOfBirth > startDate && p.DateOfBirth < endDate).OrderBy(p => p.DateOfBirth));
		
	public Task<Client?> RetrieveClientAsync(Guid id)
	{
		if (clientCache is null) return null!;
		clientCache.TryGetValue(id, out Client? client);
		return Task.FromResult(client);
	}

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

	public async Task<Client?> UpdateClientAsync(Guid id, Client c)
	{
		//обновляем в базе
		//db.Entry(c).State = EntityState.Modified;
		db.Entry(c).State = EntityState.Detached;
		db.Entry(c).State = EntityState.Modified;
		int affected = await db.SaveChangesAsync();
		if (affected == 1)
		{
			return UpdateCache(id, c);
		}
		return null;
	}

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
}