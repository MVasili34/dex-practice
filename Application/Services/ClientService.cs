using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Exceptions;
using System.Security.Principal;

namespace Services
{
	public class ClientService
	{
		private Dictionary<Client, List<Account>> clientStorage;
		public ClientService() 
		{
			clientStorage = new();
		}

		public void AddClient(Client? client) 
		{
			try
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
						clientStorage[client] = new List<Account> { new Account(new Currency("RUB", "Руб."), 0) };
					}
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
			catch(Exception ex) 
			{
				Console.WriteLine(ex.Message);
			}
		}
		public void AddAccountToClient(Client client, Account account)
		{
			try
			{
				if (!clientStorage.ContainsKey(client))
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
					clientStorage[client].Add(account);
				}
			}
			catch (IncorrectAccountException ex)
			{
				Console.WriteLine(ex.Message);
			}
			catch (ClientDoesntExistException ex)
			{ 
				Console.WriteLine(ex.Message); 
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void EditClientsAccount(Client client, int accNumber, Account account)
		{
			try
			{
				if (!clientStorage.ContainsKey(client))
				{
					throw new ClientDoesntExistException("Клиент не имеет дефолтного лицевого счёта!");
				}
				else if (String.IsNullOrEmpty(account.Currency.Name) || String.IsNullOrEmpty(account.Currency.CurrencyCode) ||
					account.Amount < 0)
				{
					throw new IncorrectAccountException("Некорректный лицевой счёт!");
				}
				else
				{
					clientStorage[client][accNumber] = account;
				}
			}
			catch (IncorrectAccountException ex)
			{
				Console.WriteLine(ex.Message);
			}
			catch (ClientDoesntExistException ex)
			{
				Console.WriteLine(ex.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public IEnumerable<Account>? RetrieveAllAccounts(Client client)
		{
			try
			{
				if (!clientStorage.ContainsKey(client))
				{
					throw new ClientDoesntExistException("Клиент не имеет дефолтного лицевого счёта!");
				}
				else
				{
					return clientStorage[client];
				}
			}
			catch (ClientDoesntExistException ex)
			{
				Console.WriteLine(ex.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return null;
		}

		public void PrintOut()
		{
			foreach (var key in clientStorage.Keys)
			{
				Console.WriteLine($"Владелец счёта: {key.FirstName}, {key.LastName}; ДР: {key.DateOfBirth}; Пасспорт: {key.Passport}; Список счетов:");
				for (int i = 0; i < clientStorage[key].Count; i++)
				{
					Console.WriteLine($"{i+1}) Код валюты: {clientStorage[key][i].Currency.CurrencyCode};" +
						$" Имя валюты: {clientStorage[key][i].Currency.Name}; Сумма: {clientStorage[key][i].Amount};");
				}
			}
		}
	}
}
