using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EntityModels;

[Table("account")]
public class Account
{
	public Account() { }

	public Account(Guid ownerId, string currencyIso, decimal amount)
	{
		AccountId = new Guid();
		OwnerId = ownerId;
		CurrencyIso = currencyIso;
		Amount = amount;
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
	public virtual Currency? CurrencyIsoNavigation { get; set; }

	[ForeignKey("OwnerId")]
	[InverseProperty("Accounts")]
	public virtual Client? Owner { get; set; }

}
