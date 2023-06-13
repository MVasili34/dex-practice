using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModels;

[Table("client")]
public class Client
{
	public Client() { }

	public Client(string FirstName, string LastName, DateOnly DateOfBirth, string Phone, 
		string Passport, string ConnectedCompany, string Adress)
	{
		ClientId = new Guid();
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
}
