using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ServicesDb;

public class RateUpdater
{
	static CancellationTokenSource cancelTokenSource = new();
	public static List<Account> accounts = null!;
	private Task? update;
	private decimal percent = 0.05M;
	private void InitializeUpdateTask()
	{
		cancelTokenSource = new CancellationTokenSource();
		update = new Task(() =>
		{
			CancellationToken token = cancelTokenSource.Token;
			while (!token.IsCancellationRequested)
			{
				//в качестве теста обновление каждые 2 секунды (для обновления раз
				//в месяц применяется метод TimeSpan.FromDays(30)
				Thread.Sleep(TimeSpan.FromSeconds(2));
				Parallel.ForEach(accounts, (account) => { account.Amount += account.Amount * percent; });
			}
		});

		update.Start();
	}

	public RateUpdater()
	{
		InitializeUpdateTask();

		//даём работать сервису 3 секунды в качестве теста
		Thread.Sleep(3000);

		cancelTokenSource.Cancel();
	}

	public RateUpdater(int delayInSeconds)
	{
		InitializeUpdateTask();

		//даём работать сервису указанное количество секунд
		Thread.Sleep(delayInSeconds * 1000);

		cancelTokenSource.Cancel();
	}

}
