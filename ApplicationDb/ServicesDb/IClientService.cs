using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModels;

namespace ServicesDb;

public interface IClientService
{
	Client? AddClient(Client c);
	int? AddAccount(Account account);
	IEnumerable<Client> RetrieveAll();
	IEnumerable<Client> GetFiltered(DateOnly startDate, DateOnly endDate);
	Client? RetrieveClientById(Guid id);
	Client? EditClient(Guid id, Client c);
	bool? DeleteClient(Guid id);
}
