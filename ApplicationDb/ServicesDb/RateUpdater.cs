using EntityModels;

namespace ServicesDb;

public class RateUpdater
{
	private static CancellationTokenSource _cancelTokenSource = new();
	private Task? _update;
	private decimal _percent = 0.05M;
    public static List<Account> accounts = null!;

    public RateUpdater()
    {
        InitializeUpdateTask();

        //даём работать сервису 3 секунды в качестве теста
        Thread.Sleep(3000);

        _cancelTokenSource.Cancel();
    }

    public RateUpdater(int delayInSeconds)
    {
        InitializeUpdateTask();

        //даём работать сервису указанное количество секунд
        Thread.Sleep(delayInSeconds * 1000);

        _cancelTokenSource.Cancel();
    }

    /// <summary>
    /// Сервис, работающий в фоне. Обновляет ставку клиентов каждые две секунды
    /// </summary>
    private void InitializeUpdateTask()
	{
		_cancelTokenSource = new CancellationTokenSource();
		_update = new Task(() =>
		{
			CancellationToken token = _cancelTokenSource.Token;
			while (!token.IsCancellationRequested)
			{
				//в качестве теста обновление каждые 2 секунды (для обновления раз
				//в месяц применяется метод TimeSpan.FromDays(30)
				Thread.Sleep(TimeSpan.FromSeconds(2));
				Parallel.ForEach(accounts, (account) => { account.Amount += account.Amount * _percent; });
			}
		});

		_update.Start();
	}
}