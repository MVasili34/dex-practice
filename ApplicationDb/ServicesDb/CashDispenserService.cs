using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModels;

namespace ServicesDb;

public class CashDispenserService
{
	private SemaphoreSlim semaphore;

	public CashDispenserService(int maxConcurrentClients)
	{
		semaphore = new SemaphoreSlim(maxConcurrentClients);
	}

	/// <summary>
	/// Сервис, обноличивающий средства нескольким клиентам в один момент времени
	/// </summary>
	/// <param name="account">Лицевой счёт</param>
	/// <param name="sum">Сумма обналичивания</param>
	/// <returns>Если сумма допустимая, то возвращается остаток на счёте</returns>
	public async Task<decimal?> DispenseCashAsync(Account account, decimal sum)
	{
		await semaphore.WaitAsync();

		try
		{
			if (account.Amount.HasValue && account.Amount >= Math.Abs(sum))
			{
				return account.Amount - sum;
			}
			return null;
		}
		finally
		{
			semaphore.Release();
		}
	}
}
