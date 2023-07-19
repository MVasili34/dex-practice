using Models;

namespace Services.Storage;

public interface IClientStorage : IStorage<Client>
{
	Dictionary<Client, List<Account>> Data { get; }
	void AddAccount(Client client, Account account);
	void UpdateAccount(Client client, int accountNumber, Account account);
	void DeleteAccount(Client client, Account account);
}