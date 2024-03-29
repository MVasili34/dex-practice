﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EntityModels;

[Table("client")]
public class Client : IPerson
{
	public Client() { }

	public Client(string firstName, string lastName, DateOnly dateOfBirth, string phone, 
		string passport, string connectedCompany, string adress)
	{
		ClientId = new Guid();
		FirstName = firstName;
		LastName = lastName;
		DateOfBirth = dateOfBirth;
		Phone = phone;
		Passport = passport;
		ConnectedCompany = connectedCompany;
		Adress = adress;
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
		Client? other = obj as Client;
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
