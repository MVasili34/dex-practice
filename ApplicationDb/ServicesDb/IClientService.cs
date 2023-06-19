﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModels;

namespace ServicesDb;

public interface IClientService
{
	Task<Client?> AddClientAsync(Client c);
	Task<int> AddAccount(Account account);
	Task<IEnumerable<Client>> RetrieveAllAsync();
	Task<IEnumerable<Client>> GetFiltered(DateOnly startDate, DateOnly endDate);
	Task<Client?> RetrieveClientAsync(Guid id);
	Task<Client?> UpdateClientAsync(Guid id, Client c);
	Task<bool?> DeleteClientAsync(Guid id);
}
