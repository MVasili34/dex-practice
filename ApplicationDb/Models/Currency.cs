using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EntityModels;

[Table("currency")]
public class Currency
{
	[Key]
	[Column("currency_code")]
	[StringLength(10)]
	public string CurrencyCode { get; set; } = null!;

	[Column("currency_name")]
	[StringLength(50)]
	public string CurrencyName { get; set; } = null!;

	[InverseProperty("CurrencyIsoNavigation")]
	public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

}
