using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EntityModels;

[Table("client")]
public class Client : IPerson
{
	public Client() { }

	public Client(string FirstName, string LastName, DateOnly DateOfBirth, string Phone, 
		string Passport, string ConnectedCompany, string Adress)
	{
		this.ClientId = new Guid();
		this.FirstName = FirstName;
		this.LastName = LastName;
		this.DateOfBirth = DateOfBirth;
		this.Phone = Phone;
		this.Passport = Passport;
		this.ConnectedCompany = ConnectedCompany;
		this.Adress = Adress;
	}

	[Key]
	[Column("client_id")]
	public Guid ClientId { get; set; }

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

	[Column("connected_company")]
	[StringLength(50)]
	public string? ConnectedCompany { get; set; }

	[Column("adress")]
	[StringLength(100)]
	public string Adress { get; set; } = null!;

	[InverseProperty("Owner")]
	public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

	public override bool Equals(object? obj)
	{
		if (obj is null)
			return false;
		if (!(obj is Client))
			return false;
		var other = obj as Client;
		return other?.ConnectedCompany == ConnectedCompany &&
			other?.ClientId == ClientId &&
			other?.Adress == Adress &&
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
			hash *= 23 + ConnectedCompany!.GetHashCode();
			hash *= 23 + Adress.GetHashCode();
			hash *= 23 + FirstName.GetHashCode();
			hash *= 23 + LastName.GetHashCode();
			hash *= 23 + Phone!.GetHashCode();
			hash *= 23 + Passport.GetHashCode();
			hash *= 23 + DateOfBirth.GetHashCode();
			hash *= 23 + ClientId.GetHashCode();
			return hash;
		}
	}
}
