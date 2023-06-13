using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModels;

[Table("account")]
public class Account
{
	public Account() { }

	public Account(Guid OwnerId, string CurrencyIso, decimal Amount)
	{
		AccountId = new Guid();
		this.OwnerId = OwnerId;
		this.CurrencyIso = CurrencyIso;
		this.Amount = Amount;
	}
	[Key]
	[Column("account_id")]
	public Guid AccountId { get; set; }

	[Column("owner_id")]
	public Guid OwnerId { get; set; }

	[Column("currency_iso")]
	[StringLength(10)]
	public string CurrencyIso { get; set; } = null!;

	[Column("amount", TypeName = "money")]
	public decimal? Amount { get; set; }

	[ForeignKey("CurrencyIso")]
	[InverseProperty("Accounts")]
	public virtual Currency CurrencyIsoNavigation { get; set; } = null!;

	[ForeignKey("OwnerId")]
	[InverseProperty("Accounts")]
	public virtual Client Owner { get; set; } = null!;

}
