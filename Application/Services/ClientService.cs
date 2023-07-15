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
		public ClientService()
		{
			this.Data = new();
		}
		public ClientService(Dictionary<Client, List<Account>> Data)
		{
			this.Data = Data;
		}

		/// <summary>
		/// Метод фильтрации клиентов по соответствующим параметрам
		/// </summary>
		/// <param name="fName">Имя</param>
		/// <param name="lName">Фамилия</param>
		/// <param name="phone">Номер телефона</param>
		/// <param name="info">Паспорт</param>
		/// <param name="sDate">Начальная дата</param>
		/// <param name="eDate">Конечная дата</param>
		/// <returns>Отфильтрованная коллекция клиентов</returns>
		public IEnumerable<KeyValuePair<Client, List<Account>>> FilterMethod(string fName, string lName, 
			string phone, string info, DateOnly sDate, DateOnly eDate) => Data.Where(p => 
			p.Key.FirstName.Contains(fName, StringComparison.OrdinalIgnoreCase) &&
			p.Key.LastName.Contains(lName, StringComparison.OrdinalIgnoreCase) &&
			p.Key.Phone.Contains(phone, StringComparison.OrdinalIgnoreCase) &&
			p.Key.Passport.Contains(info, StringComparison.OrdinalIgnoreCase) &&
			(p.Key.DateOfBirth >= sDate && p.Key.DateOfBirth <= eDate));

		/// <summary>
		/// Метод получения старейших клиентов
		/// </summary>
		/// <returns>Словарь соответствующих клиентов</returns>
		public IEnumerable<KeyValuePair<Client, List<Account>>> GetOldestClients() => Data.Where(p => 
			p.Key.DateOfBirth == Data.Min(t => t.Key.DateOfBirth));

        /// <summary>
        /// Метод получения самых молодых клиентов
        /// </summary>
        /// <returns>Словарь соответствующих клиентов</returns>
        public IEnumerable<KeyValuePair<Client, List<Account>>> GetYoungestClients()=> Data.Where(p => 
			p.Key.DateOfBirth == Data.Max(t => t.Key.DateOfBirth));

        /// <summary>
        /// Метод получения среднего возраста клиентов
        /// </summary>
        /// <returns>Средний возраст</returns>
        public int GetAvarageAge() => DateTime.Now.DayOfYear < Data.Keys.Average(p => p.DateOfBirth.DayOfYear) ?
			DateTime.Now.Year - (int)Data.Keys.Average(p => p.DateOfBirth.Year) :
			DateTime.Now.Year - (int)Data.Keys.Average(p => p.DateOfBirth.Year) - 1;

		/// <summary>
		/// Метод добавления клиента в хранилище
		/// </summary>
		/// <param name="client">Клиент</param>
		/// <exception cref="Below18Exception"></exception>
		/// <exception cref="PassportNullException"></exception>
		public void Add(Client? client) 
		{
			if (client is not null) 
			{
				if ((client.DateOfBirth.DayOfYear<=DateTime.Now.DayOfYear ? DateTime.Now.Year-client.DateOfBirth.Year : 
					DateTime.Now.Year - client.DateOfBirth.Year - 1) < 18)
				{
					throw new Below18Exception("Клиент не может быть младше 18 лет");
				}
				else if (string.IsNullOrEmpty(client.Passport))
				{
					throw new PassportNullException("У клиента нет паспортных данных");
				}
				Data[client] = new List<Account> { new Account(new Currency("RUB", "Руб."), 0) };
			}
		}

		/// <summary>
		/// Метод добавления лицевого счёта клиенту
		/// </summary>
		/// <param name="client">Клиент</param>
		/// <param name="account">Аккаунт</param>
		/// <exception cref="ClientDoesntExistException"></exception>
		/// <exception cref="IncorrectAccountException"></exception>
		public void AddAccount(Client client, Account account)
		{
			if (!Data.ContainsKey(client))
			{
				throw new ClientDoesntExistException("Клиент не зарегистрирован!");
			}
			else if (string.IsNullOrEmpty(account.Currency.CurrencyName) || string.IsNullOrEmpty(account.Currency.CurrencyCode) ||
				account.Amount<0)
			{
				throw new IncorrectAccountException("Некорректный лицевой счёт!");
			}
			Data[client].Add(account);
		}

		/// <summary>
		/// Метод обновления лицевого счёта
		/// </summary>
		/// <param name="client">Клиент</param>
		/// <param name="accNumber">Номер счёта</param>
		/// <param name="account">Обновлённый счёт</param>
		/// <exception cref="ClientDoesntExistException"></exception>
		/// <exception cref="IncorrectAccountException"></exception>
		public void UpdateAccount(Client client, int accNumber, Account account)
		{
			if (!Data.ContainsKey(client))
			{
				throw new ClientDoesntExistException("Клиент не имеет дефолтного лицевого счёта!");
			}
			else if (string.IsNullOrEmpty(account.Currency.CurrencyName) || string.IsNullOrEmpty(account.Currency.CurrencyCode))
			{
				throw new IncorrectAccountException("Некорректный лицевой счёт!");
			}
			Data[client][accNumber] = account;
		}

		/// <summary>
		/// Метод удаления клиента из хранилища
		/// </summary>
		/// <param name="client">Клиент</param>
		/// <exception cref="FailedToRemoveException"></exception>
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

		/// <summary>
		/// Метод удаления лицевого счёта клиента
		/// </summary>
		/// <param name="client">Клиент</param>
		/// <param name="account">Счёт</param>
		/// <exception cref="FailedToRemoveException"></exception>
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
	}
}
