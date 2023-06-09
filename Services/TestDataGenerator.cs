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
        public static IEnumerable<Client> GenerateClints(int amount)
        {
            var generate = new Faker<Client>("ru")
                .RuleFor(b => b.FirstName, t => t.Name.FirstName())
                .RuleFor(b => b.LastName, t => t.Name.LastName())
                .RuleFor(b => b.DateOfBirth, t => t.Date.BetweenDateOnly(DateOnly.FromDateTime(DateTime.Now.AddYears(-80).Date),
                DateOnly.FromDateTime(DateTime.Now.AddYears(-18).Date)))
                .RuleFor(b => b.Phone, t => t.Phone.PhoneNumberFormat())
                .RuleFor(b => b.Passport, t => "AB"+t.Random.Byte().ToString())
                .RuleFor(b => b.Company, t => t.Company.CompanyName())
                .RuleFor(b => b.AdressInfo, t => t.Address.FullAddress());
            return generate.Generate(amount);
        }

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
    }
}
