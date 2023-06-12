using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Storage
{
	public interface IClientStorage : IStorage<Client>
	{
		Dictionary<Client, List<Account>> Data { get; }
		void AddAccount(Client client, Account account);
		void UpdateAccount(Client client, int accNumber, Account account);
		void DeleteAccount(Client client, Account account);

	}
}
