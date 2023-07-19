
namespace EntityModels;

public interface IPerson
{
	string FirstName { get; set; }
	string LastName { get; set; }
	DateOnly DateOfBirth { get; set; }
	string? Phone { get; set; }
	string Passport { get; set; }
}
