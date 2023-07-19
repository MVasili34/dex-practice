
namespace Models;

public class Employee : Person
{

	private string _contract = string.Empty;
	private string _position = string.Empty;
	private int _salary;

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
		get => _contract;
		set => _contract = value;
	}

	public string Position
	{
		get => _position;
		set => _position = value;
	}

	public int Salary
	{
		get => _salary;
		set => _salary = value;
	}
	
	public override bool Equals(object? obj)
	{
		if (obj is null)
			return false;
		if (!(obj is Employee))
			return false;
		Employee? other = obj as Employee;
		return other?.Contract == Contract &&
			other.Position == Position &&
			other.Salary == Salary &&
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
			hash *= 23 + Contract.GetHashCode();
			hash *= 23 + Position.GetHashCode();
			hash *= 23 + Salary.GetHashCode();
			hash *= 23 + FirstName.GetHashCode();
			hash *= 23 + LastName.GetHashCode();
			hash *= 23 + Phone.GetHashCode();
			hash *= 23 + Passport.GetHashCode();
			hash *= 23 + DateOfBirth.GetHashCode();
			return hash;
		}
	}  
}
