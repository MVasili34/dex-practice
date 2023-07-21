using Models;
using Services.Exceptions;

namespace Services.Storage;

public class ClientStorage
{
	public Dictionary<Client, List<Account>> Clients { get; }

	public ClientStorage()
	{
		Clients = TestDataGenerator.GenerateClientsWithAccounts()
			.ToDictionary(pair => pair.Key, pair => pair.Value);
	}

	public void AddClient(Client client)
	{
		if (client is not null)
		{
			if (DateTime.Now.Year - client.DateOfBirth.Year < 18)
			{
				throw new Below18Exception("Клиент не может быть младше 18 лет");
			}
			else if (string.IsNullOrEmpty(client.Passport))
			{
				throw new PassportNullException("У клиента нет паспортных данных");
			}
			Clients[client] = new List<Account> { new Account(new Currency("RUB", "Руб."), 0) };
		}
	}
}
