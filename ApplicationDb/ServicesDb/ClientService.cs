using EntityModels;
using Microsoft.EntityFrameworkCore;

namespace ServicesDb;

public class ClientService : IClientService
{

	private readonly BankingServiceContext _bankContext;

	private readonly int _pageSize = 10;

	public ClientService(BankingServiceContext bankContext)
	{
		_bankContext = bankContext;
	}

    /// <summary>
    /// Метод добавления клиента в БД
    /// </summary>
    /// <param name="client">Клиент</param>
    /// <returns>Клиент, если процесс добавления прошёл успешно</returns>
    public async Task<Client?> AddClientAsync(Client client)
	{
		await _bankContext.Clients.AddAsync(client);
		int affected = await _bankContext.SaveChangesAsync();
		if (affected == 1)
		{
			await _bankContext.AddAsync(new Account(client.ClientId, "RUB", 0));
			await _bankContext.SaveChangesAsync();
			return await RetrieveClientAsync(client.ClientId);
		}
			return null;
	}

	/// <summary>
	/// Метод добавления лицевого счёта в БД
	/// </summary>
	/// <param name="account">Лицевой счёт</param>
	/// <returns>Лицевой счёт, иначе null</returns>
    public async Task<Account?> AddAccountAsync(Account account)
    {
        await _bankContext.Accounts.AddAsync(account);
        int affected = await _bankContext.SaveChangesAsync();
        if (affected == 1)
        {
            return await _bankContext.Accounts.FindAsync(account.AccountId);
        }
			return null;
    }

    /// <summary>
    /// Метод получения клиентов из БД (с применением пагинации)
    /// </summary>
    /// <param name="page">Номер страницы</param>
    /// <returns>Клиенты из БД вместе с лицевыми счетами</returns>
    public async Task<IEnumerable<Client>> RetrieveAllAsync(int? page) => await _bankContext.Clients
		.Include(p => p.Accounts)
		.Skip(((page ?? 1) - 1) * _pageSize)
		.Take(_pageSize)
		.ToListAsync();
	
	/// <summary>
	/// Метод получения клиента по идентификатору
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>Клиент из базы данных</returns>
	public async Task<Client?> RetrieveClientAsync(Guid id) => await _bankContext.Clients
		.Include(p => p.Accounts).FirstOrDefaultAsync(t => t.ClientId == id);

	/// <summary>
	/// Метод обновления данных клиента
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <param name="client">Клиент</param>
	/// <returns>Клиент, если успешно обновлён</returns>
	public async Task<Client?> UpdateClientAsync(Guid id, Client client)
	{
        Client? existingClient = await _bankContext.Clients.FindAsync(id);
		if (existingClient is not null)
		{
			_bankContext.Entry(existingClient).CurrentValues.SetValues(client);
			int affected = await _bankContext.SaveChangesAsync();
			if (affected == 1)
			{
				return await RetrieveClientAsync(id);
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
		Client? client = await _bankContext.Clients.FindAsync(id);
		if (client is null) 
			return null;
		_bankContext.Clients.Remove(client);
		int affected = await _bankContext.SaveChangesAsync();

		//удаление клиента вместе со счетами
		if (affected >= 1)
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
        Account? account = await _bankContext.Accounts.FindAsync(id);
        if (account is null) 
			return null;
        _bankContext.Accounts.Remove(account);
        int affected = await _bankContext.SaveChangesAsync();
        if (affected == 1)
        {
            return true;
        }
			return null;
    }
}