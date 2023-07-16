using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModels;

namespace ServicesDb;

public interface IClientService
{
	Task<Client?> AddClientAsync(Client client);
    Task<bool?> DeleteAccountAsync(Guid id);
    Task<IEnumerable<Client>> RetrieveAllAsync();
	Task<IEnumerable<Client>> GetFilteredAsync(DateOnly startDate, DateOnly endDate);
	Task<Client?> RetrieveClientAsync(Guid id);
	Task<Client?> UpdateClientAsync(Guid id, Client client);
	Task<bool?> DeleteClientAsync(Guid id);
}
