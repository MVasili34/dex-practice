using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using EntityModels;

namespace ServicesDb;

public class DataGenerator
{
	public static IEnumerable<Client> GenerateClints(int amount) => Enumerable.Range(0, amount).Select(p => GenereteClient());

	public static IEnumerable<Account> GenerateAccounts(int amount) => Enumerable.Range(0, amount).Select(p => GenerateAccount());

	public static IEnumerable<Employee> GenerateEmployees(int amount) => Enumerable.Range(0, amount).Select(p => GenerateEmployee());

	public static Account GenerateAccount() => new Faker<Account>("ru")
		.RuleFor(b => b.AccountId, t => new Guid())
		.RuleFor(b => b.OwnerId, t => new Guid())
		.RuleFor(b => b.CurrencyIso, t=> t.Finance.Currency().Code)
		.RuleFor(b => b.Amount, t => t.Finance.Random.Decimal(100, 10000));

	public static Employee GenerateEmployee() => new Faker<Employee>("ru")
			.RuleFor(b => b.FirstName, t => t.Name.FirstName())
			.RuleFor(b => b.LastName, t => t.Name.LastName())
			.RuleFor(b => b.DateOfBirth, t => t.Date.BetweenDateOnly(DateOnly.FromDateTime(DateTime.Now.AddYears(-80).Date),
			DateOnly.FromDateTime(DateTime.Now.AddYears(-18).Date)))
			.RuleFor(b => b.Phone, t => t.Phone.PhoneNumberFormat())
			.RuleFor(b => b.Passport, t => "AB" + t.Random.Byte().ToString())
			.RuleFor(b => b.Position, t => t.Random.Word())
			.RuleFor(b => b.Salary, t => Math.Max((int)t.Random.Short(), 1000))
			.RuleFor(b => b.Contract, t => "Заключён").Generate();
	public static Client GenereteClient() => new Faker<Client>("ru")
	.RuleFor(b => b.FirstName, t => t.Name.FirstName())
		.RuleFor(b => b.LastName, t => t.Name.LastName())
		.RuleFor(b => b.DateOfBirth, t => t.Date.BetweenDateOnly(DateOnly.FromDateTime(DateTime.Now.AddYears(-80).Date),
		DateOnly.FromDateTime(DateTime.Now.AddYears(-18).Date)))
		.RuleFor(b => b.Phone, t => t.Phone.PhoneNumberFormat())
		.RuleFor(b => b.Passport, t => "AB" + t.Random.Byte().ToString())
		.RuleFor(b => b.ConnectedCompany, t => t.Company.CompanyName())
		.RuleFor(b => b.Adress, t => t.Address.City()).Generate();
}
