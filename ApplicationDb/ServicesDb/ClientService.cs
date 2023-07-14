using EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace ServicesDb;

public class ClientService : IClientService
{

	private BankingServiceContext db;
	public ClientService(BankingServiceContext db)
	{
		this.db = db;
	}

    /// <summary>
    /// Метод добавления клиента в БД
    /// </summary>
    /// <param name="client">Клиент</param>
    /// <returns>Клиент, если процесс добавления прошёл успешно</returns>
    public async Task<Client?> AddClientAsync(Client client)
	{
		await db.Clients.AddAsync(client);
		int affected = await db.SaveChangesAsync();
		if (affected == 1)
		{
			await db.AddAsync(new Account(client.ClientId, "RUB", 0));
			await db.SaveChangesAsync();
			return db.Clients.Find(client.ClientId);
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
	/// Метод получения всех клиентов из БД
	/// </summary>
	/// <returns>Содержимое базы данных</returns>
    public async Task<IEnumerable<Client>> RetrieveAllAsync() => await db.Clients
		.Include(p => p.Accounts).ToListAsync();

    /// <summary>
    /// Метод фильтрации клиентов по дате рождения
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns>Отфильтрованная коллекция</returns>
    public async Task<IEnumerable<Client>> GetFiltered(DateOnly startDate, DateOnly endDate) => await db.Clients
		.Where(p => p.DateOfBirth > startDate && p.DateOfBirth < endDate)
		.OrderBy(p => p.DateOfBirth).ToListAsync();
	
	/// <summary>
	/// Метод получения клиента по идентификатору
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Клиент из базы данных</returns>
	public async Task<Client?> RetrieveClientAsync(Guid id) => await db.Clients.FindAsync(id);

	/// <summary>
	/// Метод обновления данных клиента
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <param name="client">Клиент</param>
	/// <returns>Клиент, если успешно обновлён</returns>
	public async Task<Client?> UpdateClientAsync(Guid id, Client client)
	{
        Client? existingClient = await db.Clients.FindAsync(id);
		if (existingClient is not null)
		{
			db.Entry(existingClient).CurrentValues.SetValues(client);
			int affected = await db.SaveChangesAsync();
			if (affected == 1)
			{
				return db.Clients.Find(id);
			}
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
		Client? client = db.Clients.Find(id);
		if (client is null) 
			return null;
		db.Clients.Remove(client);
		int affected = await db.SaveChangesAsync();
		if (affected == 1)
		{
			return true;
		}
			return null;
	}

	/// <summary>
	/// Метод удаления лицевого счёта по идентификатору
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Статус операции</returns>
    public async Task<bool?> DeleteAccountAsync(Guid id)
    {
        Account? c = db.Accounts.Find(id);
        if (c is null) 
			return null;
        db.Accounts.Remove(c);
        int affected = await db.SaveChangesAsync();
        if (affected == 1)
        {
            return true;
        }
			return null;
    }
}