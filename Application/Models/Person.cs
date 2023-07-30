namespace Models;

public class Person
{

	private string _firstName = string.Empty;
	private string _lastName = string.Empty;
	private DateOnly _birthDate;
	private string _phoneNumber = string.Empty;
	private string _passportNumber = string.Empty;

	public Person() { }

	public Person(string firstName, string lastName, DateOnly dateOfBirth, string phone, string passport)
	{
		FirstName = firstName;
		LastName = lastName;
		DateOfBirth = dateOfBirth;
		Phone = phone;
		Passport = passport;
	}

	public string FirstName
	{
		get => _firstName;
		set => _firstName = value;
	}

	public string LastName
	{
		get => _lastName;
		set => _lastName = value;
	}

	public DateOnly DateOfBirth
	{
		get => _birthDate;
		set => _birthDate = value;
	}

	public string Phone
	{
		get => _phoneNumber;
		set => _phoneNumber = value;
	}

	public string Passport
	{
		get => _passportNumber;
		set => _passportNumber = value;
	}
}
