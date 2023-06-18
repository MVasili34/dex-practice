using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModels;

namespace ServicesDb;

public interface IClientService
{
	Task<Client?> AddClient(Client c);
	Task<int> AddAccount(Account account);
	Task<IEnumerable<Client>> RetrieveAll();
	Task<IEnumerable<Client>> GetFiltered(DateOnly startDate, DateOnly endDate);
	Task<Client?> RetrieveClientById(Guid id);
	Task<Client?> EditClient(Guid id, Client c);
	Task<bool?> DeleteClient(Guid id);
}
