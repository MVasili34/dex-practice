using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace EntityModels;

[Table("employee")]
public class Employee : IPerson
{
	public Employee() { }

	public Employee(string FirstName, string LastName, DateOnly DateOfBirth, string Phone,
		string Passport, string Position, decimal Salary, string Contract)
	{
		EmployeeId = new Guid();
		this.FirstName = FirstName;
		this.LastName = LastName;
		this.DateOfBirth = DateOfBirth;
		this.Phone = Phone;
		this.Passport = Passport;
		this.Position = Position;
		this.Salary = Salary;
		this.Contract = Contract;
	}
	[Key]
	[Column("employee_id")]
	public Guid EmployeeId { get; set; }

	[Column("first_name")]
	[StringLength(50)]
	public string FirstName { get; set; } = null!;

	[Column("last_name")]
	[StringLength(50)]
	public string LastName { get; set; } = null!;

	[Column("date_of_birth")]
	public DateOnly DateOfBirth { get; set; }

	[Column("phone")]
	[StringLength(50)]
	public string? Phone { get; set; }

	[Column("passport")]
	[StringLength(50)]
	public string Passport { get; set; } = null!;

	[Column("position")]
	[StringLength(50)]
	public string Position { get; set; } = null!;

	[Column("salary", TypeName = "money")]
	public decimal Salary { get; set; }

	[Column("contract")]
	[StringLength(100)]
	public string? Contract { get; set; }

	public override bool Equals(object? obj)
	{
		if (obj is null)
			return false;
		if (!(obj is Employee))
			return false;
		var other = obj as Employee;
		return other?.Position == Position &&
			other?.EmployeeId == EmployeeId &&
			other?.Contract == Contract &&
			other?.Salary == Salary &&
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
			hash *= 23 + Position.GetHashCode();
			hash *= 23 + Salary.GetHashCode();
			hash *= 23 + Contract!.GetHashCode();
			hash *= 23 + FirstName.GetHashCode();
			hash *= 23 + LastName.GetHashCode();
			hash *= 23 + Phone!.GetHashCode();
			hash *= 23 + Passport.GetHashCode();
			hash *= 23 + DateOfBirth.GetHashCode();
			hash *= 23 + EmployeeId.GetHashCode();
			return hash;
		}
	}
}