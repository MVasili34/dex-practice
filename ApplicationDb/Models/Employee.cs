using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace EntityModels;

[Table("employee")]
public class Employee
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
}