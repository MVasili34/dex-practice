using Models;

namespace Services
{
	public class BankService
	{
		public static decimal Income { get; set; } = 250_000;
		public static decimal Expense { get; set; } = 200_000;

		public BankService() { }

		public static int OwnerPayment(Employee employee, int amountofOwners) => amountofOwners != 0 ?
			(int)(Income - Expense) / amountofOwners : 0;
	}
}