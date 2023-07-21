
namespace Models;

public class Client : Person
{
	private string _connectedCompany = string.Empty;
	private string _contactInfo = string.Empty;

	public Client() { }

	public Client(string firstName, string lastName, DateOnly dateOfBirth, string phone, string passport,
		string? company, string addressInfo) : base(firstName, lastName, dateOfBirth, phone, passport)
	{
		Company = company;
		AddressInfo = addressInfo;
	}

	public string? Company
	{
		get => _connectedCompany;
		set => _connectedCompany = value!;
	}

	public string AddressInfo
	{
		get => _contactInfo;
		set => _contactInfo = value;
	}

	public override bool Equals(object? obj)
	{
		if (obj is null)
			return false;
		if (!(obj is Client))
			return false;
		Client? other = obj as Client;
		return other?.Company == Company &&
			other?.AddressInfo == AddressInfo &&
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
			hash *= 23 + _connectedCompany.GetHashCode();
			hash *= 23 + _contactInfo.GetHashCode();
			hash *= 23 + FirstName.GetHashCode();
			hash *= 23 + LastName.GetHashCode();
			hash *= 23 + Phone.GetHashCode();
			hash *= 23 + Passport.GetHashCode();
			hash *= 23 + DateOfBirth.GetHashCode();
			return hash;
		}
	} 

	//метод осуществления явного приведения Клиента к Сотруднику банка
	public static explicit operator Employee(Client client) => new Employee(client.FirstName, 
		client.LastName, client.DateOfBirth, client.Phone, client.Passport, "Стажёр", 1000);
}