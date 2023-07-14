using Models;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Storage
{
	public class ClientStorage
	{
		public readonly Dictionary<Client, List<Account>> clients;

		public ClientStorage() => clients = TestDataGenerator.GenerateAccounts()
			.ToDictionary(pair => pair.Key, pair => pair.Value);

		public void AddClient(Client? client)
		{
			try
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
						clients[client] = new List<Account> { new Account(new Currency("RUB", "Руб."), 0) };
				}
			}
			catch (PassportNullException ex)
			{
				Console.WriteLine(ex.Message);
			}
			catch (Below18Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
