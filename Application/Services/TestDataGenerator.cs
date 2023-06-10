using Bogus;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TestDataGenerator
    {
        public static IEnumerable<Client> GenerateClints(int amount) => ClientRule().Generate(amount);

		public static IDictionary<string, Client> GenerateClintesDictionary(IEnumerable<Client> values)
        {
            Dictionary<string, Client> newdictionary = new();
            foreach (var value in values) 
            {
                newdictionary[value.Phone] = value;
            }
            return newdictionary;
        }

        public static IEnumerable<Employee> GenerateEmployees(int amount)
        {
            var generate = new Faker<Employee>("ru")
                .RuleFor(b => b.FirstName, t => t.Name.FirstName())
                .RuleFor(b => b.LastName, t => t.Name.LastName())
                .RuleFor(b => b.DateOfBirth, t => t.Date.BetweenDateOnly(DateOnly.FromDateTime(DateTime.Now.AddYears(-80).Date),
                DateOnly.FromDateTime(DateTime.Now.AddYears(-18).Date)))
                .RuleFor(b => b.Phone, t => t.Phone.PhoneNumberFormat())
				.RuleFor(b => b.Passport, t => "AB" + t.Random.Byte().ToString())
				.RuleFor(b => b.Position, t => t.Random.Word())
                .RuleFor(b => b.Salary, t => Math.Max((int)t.Random.Short(), 1000));
            return generate.Generate(amount);
        }

		public static IDictionary<Client, List<Account>> GenerateAccounts()
		{
			var dictionary = new Dictionary<Client, List<Account>>();
			var generateaccount = new Faker<Account>("ru")
				.RuleFor(b => b.Currency, t => new Currency(t.Finance.Currency().Code, t.Finance.Currency().Description))
				.RuleFor(b => b.Amount, t => t.Random.Int());
			for (int i = 0; i < 10; i++)
			{
				dictionary[ClientRule().Generate()] = new List<Account>(Enumerable.Range(1, Random.Shared.Next(1, 3))
					.Select(index => generateaccount.Generate()));
			}
			return dictionary;
		}

		private static Faker<Client> ClientRule() => new Faker<Client>("ru")
			.RuleFor(b => b.FirstName, t => t.Name.FirstName())
				.RuleFor(b => b.LastName, t => t.Name.LastName())
				.RuleFor(b => b.DateOfBirth, t => t.Date.BetweenDateOnly(DateOnly.FromDateTime(DateTime.Now.AddYears(-80).Date),
				DateOnly.FromDateTime(DateTime.Now.AddYears(-18).Date)))
				.RuleFor(b => b.Phone, t => t.Phone.PhoneNumberFormat())
				.RuleFor(b => b.Passport, t => "AB" + t.Random.Byte().ToString())
				.RuleFor(b => b.Company, t => t.Company.CompanyName())
				.RuleFor(b => b.AdressInfo, t => t.Address.FullAddress());
	}
}
