using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class Employee : Person
	{
		private string contract = String.Empty;
		private string position = String.Empty;
		private int salary;

		public Employee() { }
		public Employee(string FirstName, string LastName, DateOnly DateOfBirth, string Phone, string Passport,
			string Position, int Salary) : base(FirstName, LastName, DateOfBirth, Phone, Passport)
		{
			this.Position = Position;
			this.Salary = Salary;
			Contract = $"Контракт заключён с {FirstName}, {LastName}. Дата и время: {DateTime.Now}";
		}
		public string Contract
		{
			get => contract;
			set => contract = value;
		}
		public string Position
		{
			get => position;
			set => position = value;
		}
		public int Salary
		{
			get => salary;
			set => salary = value;
		}
	}
}
