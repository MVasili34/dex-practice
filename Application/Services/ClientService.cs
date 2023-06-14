using Models;
using System;
using Services.Storage;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Exceptions;
using System.Security.Principal;

namespace Services
{
	public class ClientService : IClientStorage
	{
		public Dictionary<Client, List<Account>> Data { get; }
		public ClientService() => Data = new();
		public ClientService(Dictionary<Client, List<Account>> clientStorage) => Data = clientStorage;

		public IEnumerable<KeyValuePair<Client, List<Account>>> FilterMethod(string fName, string lName, string phone, string info,
		  DateOnly sDate, DateOnly eDate) => Data.Where(p => 
			p.Key.FirstName.Contains(fName, StringComparison.OrdinalIgnoreCase) &&
			p.Key.LastName.Contains(lName, StringComparison.OrdinalIgnoreCase) &&
			p.Key.Phone.Contains(phone, StringComparison.OrdinalIgnoreCase) &&
			p.Key.Passport.Contains(info, StringComparison.OrdinalIgnoreCase) &&
			(p.Key.DateOfBirth >= sDate && p.Key.DateOfBirth <= eDate));

		public IEnumerable<KeyValuePair<Client, List<Account>>> GetOldestClients() => Data.Where(p => 
		p.Key.DateOfBirth == Data.Min(t => t.Key.DateOfBirth));

		public IEnumerable<KeyValuePair<Client, List<Account>>> GetYoungestClients()=> Data.Where(p => 
		p.Key.DateOfBirth == Data.Max(t => t.Key.DateOfBirth));

		public int GetAvarageAge() => DateTime.Now.DayOfYear < Data.Keys.Average(p => p.DateOfBirth.DayOfYear) ?
			DateTime.Now.Year - (int)Data.Keys.Average(p => p.DateOfBirth.Year) :
			DateTime.Now.Year - (int)Data.Keys.Average(p => p.DateOfBirth.Year) - 1;

		public void Add(Client? client) 
		{
				if (client is not null) 
				{
					if ((client.DateOfBirth.DayOfYear<=DateTime.Now.DayOfYear ? DateTime.Now.Year-client.DateOfBirth.Year : 
						DateTime.Now.Year - client.DateOfBirth.Year - 1) < 18)
					{
						throw new Below18Exception("Клиент не может быть младше 18 лет");
					}
					else if (String.IsNullOrEmpty(client.Passport))
					{
						throw new PassportNullException("У клиента нет паспортных данных");
					}
					else
					{
						Data[client] = new List<Account> { new Account(new Currency("RUB", "Руб."), 0) };
					}
				}
		}
		public void AddAccount(Client client, Account account)
		{
				if (!Data.ContainsKey(client))
				{
					throw new ClientDoesntExistException("Клиент не зарегистрирован!");
				}
				else if (String.IsNullOrEmpty(account.Currency.Name) || String.IsNullOrEmpty(account.Currency.CurrencyCode) ||
					account.Amount<0)
				{
					throw new IncorrectAccountException("Некорректный лицевой счёт!");
				}
				else
				{
					Data[client].Add(account);
				}
		}

		public void UpdateAccount(Client client, int accNumber, Account account)
		{
				if (!Data.ContainsKey(client))
				{
					throw new ClientDoesntExistException("Клиент не имеет дефолтного лицевого счёта!");
				}
				else if (String.IsNullOrEmpty(account.Currency.Name) || String.IsNullOrEmpty(account.Currency.CurrencyCode))
				{
					throw new IncorrectAccountException("Некорректный лицевой счёт!");
				}
				else
				{
					Data[client][accNumber] = account;
				}
		}

		public void Delete(Client? client)
		{

				if (client is not null)
				{
					if (!Data.Remove(client))
						throw new FailedToRemoveException("Невозможнло удалить клиента!");
				}
				else
					throw new FailedToRemoveException("Невозможнло удалить клиента!");
		}

		public void DeleteAccount(Client client, Account account)
		{
				if (client is not null && account is not null)
				{
					if (!Data[client].Remove(account))
						throw new FailedToRemoveException("Невозможнло удалить cчёт клиента!");
				}
				else
					throw new FailedToRemoveException("Невозможнло удалить счёт клиента!");
		}


		public IEnumerable<Account>? RetrieveAllAccounts(Client client)
		{

				if (!Data.ContainsKey(client))
				{
					throw new ClientDoesntExistException("Клиент не имеет дефолтного лицевого счёта!");
				}
				else
				{
					return Data[client];
				}
		}

		public void PrintOut()
		{
			foreach (var key in Data.Keys)
			{
				Console.WriteLine($"Владелец счёта: {key.FirstName}, {key.LastName}; ДР: {key.DateOfBirth}; Пасспорт: {key.Passport}; Список счетов:");
				for (int i = 0; i < Data[key].Count; i++)
				{
					Console.WriteLine($"{i+1}) Код валюты: {Data[key][i].Currency.CurrencyCode};" +
						$" Имя валюты: {Data[key][i].Currency.Name}; Сумма: {Data[key][i].Amount};");
				}
			}
		}

		public void PrintOut(Dictionary<Client, List<Account>> toprint)
		{
			foreach (var key in toprint.Keys)
			{
				Console.WriteLine($"Владелец счёта: {key.FirstName}, {key.LastName}; ДР: {key.DateOfBirth}; Пасспорт: {key.Passport}; Список счетов:");
				for (int i = 0; i < Data[key].Count; i++)
				{
					Console.WriteLine($"{i + 1}) Код валюты: {Data[key][i].Currency.CurrencyCode};" +
						$" Имя валюты: {Data[key][i].Currency.Name}; Сумма: {Data[key][i].Amount};");
				}
			}
		}
	}
}
