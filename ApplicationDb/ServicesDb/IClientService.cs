using EntityModels;

namespace ServicesDb;

public interface IClientService
{
	Task<Client?> AddClientAsync(Client client);
	Task<Account?> AddAccountAsync(Account account);
    Task<bool?> DeleteAccountAsync(Guid id);
    Task<IEnumerable<Client>> RetrieveAllAsync(int? page);
	Task<Client?> RetrieveClientAsync(Guid id);
	Task<Client?> UpdateClientAsync(Guid id, Client client);
	Task<bool?> DeleteClientAsync(Guid id);
}
