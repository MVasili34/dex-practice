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


	public Client? AddClient(Client c)
	{
		EntityEntry<Client> added = db.Clients.Add(c);
		int affected = db.SaveChanges();
		if (affected == 1)
		{
			if (clientCache is null)
				return c;
			db.Add(new Account(c.ClientId, "RUB", 0));
			db.SaveChanges();
			//нового клиента в кэш, иначе вызываем UpdateCache
			return clientCache.AddOrUpdate(c.ClientId, c, UpdateCache);
		}
			return null;
	}

	public int? AddAccount(Account account)
	{
		EntityEntry<Account> added = db.Accounts.Add(account);
		return db.SaveChanges();
	}

	public IEnumerable<Client> RetrieveAll() => clientCache is null ?
		Enumerable.Empty<Client>() : clientCache.Values;

	public IEnumerable<Client> GetFiltered(DateOnly startDate, DateOnly endDate) => clientCache is null ?
		Enumerable.Empty<Client>() : clientCache.Values.Where(p =>
		p.DateOfBirth > startDate && p.DateOfBirth < endDate).OrderBy(p => 
		p.DateOfBirth);
		

	public Client? RetrieveClientById(Guid id)
	{
		if (clientCache is null) return null!;
		clientCache.TryGetValue(id, out Client? client);
		return client;
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

	public Client? EditClient(Guid id, Client c)
	{
		//обновляем в базе
		db.Entry(c).State = EntityState.Modified;
		int affected = db.SaveChanges();
		if (affected == 1)
		{
			return UpdateCache(id, c);
		}
		return null;
	}

	public bool? DeleteClient(Guid id)
	{
		Client? c = db.Clients.Find(id);
		if (c is null) return null;
		db.Clients.Remove(c);
		int affected = db.SaveChanges();
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