using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class Employee : Person
	{

		private string contract = string.Empty;
		private string position = string.Empty;
		private int salary;

		public Employee() { }

		public Employee(string FirstName, string LastName, DateOnly DateOfBirth, string Phone, string Passport,
			string Position, int Salary) : base(FirstName, LastName, DateOfBirth, Phone, Passport)
		{
			this.Position = Position;
			this.Salary = Salary;
			this.Contract = $"Контракт заключён с {FirstName}, {LastName}. Дата и время: {DateTime.Now}";
		}

		public Employee(string FirstName, string LastName, DateOnly DateOfBirth, string Phone, string Passport,
			string Position, int Salary, string Contract) : base(FirstName, LastName, DateOfBirth, Phone, Passport)
		{
			this.Position = Position;
			this.Salary = Salary;
			this.Contract = Contract;
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
		
		public override bool Equals(object? obj)
		{
			if (obj is null)
				return false;
			if (!(obj is Employee))
				return false;
			var other = obj as Employee;
			return other?.contract == contract &&
				other.position == position &&
				other.salary == salary &&
				other.FirstName == FirstName &&
				other.LastName == LastName &&
				other.Phone == Phone &&
				other.Passport == Passport &&
				other.DateOfBirth.CompareTo(DateOfBirth) == 0;
		}
	    
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash *= 23 + contract.GetHashCode();
				hash *= 23 + position.GetHashCode();
				hash *= 23 + salary.GetHashCode();
				hash *= 23 + FirstName.GetHashCode();
				hash *= 23 + LastName.GetHashCode();
				hash *= 23 + Phone.GetHashCode();
				hash *= 23 + Passport.GetHashCode();
				hash *= 23 + DateOfBirth.GetHashCode();
				return hash;
			}
		}  
	}
}
