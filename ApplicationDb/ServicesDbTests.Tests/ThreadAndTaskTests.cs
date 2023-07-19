using ExportTool;
using EntityModels;
using ServicesDb;
using Microsoft.Extensions.DependencyInjection;

namespace ServicesDbTests.Tests;

public class ThreadAndTaskTests
{
	[Fact] //параллельный экспорт и импорт клиентов из БД
	public void ParallelExportImportClientsTest()
	{
		ManualResetEvent exportCompleted = new(false);
		ManualResetEvent importCompleted = new(false);
		ExportService<Client> exportService = new(Environment.CurrentDirectory, "export.csv");
		ExportService<Client> importService = new(Environment.CurrentDirectory, "import.csv");
		BankingServiceContext serviceContext = new();
		importService.ExportPersons(serviceContext.Clients);

		ThreadPool.QueueUserWorkItem(_ =>
		{
			using (var export = new BankingServiceContext())
			{
				exportService.ExportPersons(export.Clients);
				export.Clients.RemoveRange(export.Clients);
				export.SaveChanges();
				exportCompleted.Set();
			}
		});
		ThreadPool.QueueUserWorkItem(_ =>
		{
			using (var import = new BankingServiceContext())
			{
				import.AddRange(importService.ImportPersons()!);
				import.SaveChanges();
				importCompleted.Set();
			}
		});
		WaitHandle.WaitAll(new[] { exportCompleted, importCompleted });

		bool? difference = exportService.ImportPersons()!.Except(serviceContext.Clients).Any();
        Assert.False(difference);
	}

	[Fact]
	public void ParallelIncreaseTest()
	{
		Account account = new(new Guid(), "USD", 0);
		decimal? amount = account.Amount;
		object sync = new();

		Parallel.For(0, 2, _ =>
		{
			for (int i = 0; i < 10; i++)
			{
				lock (sync)
				{
					account.Amount += 100;
				}
			}
		});

		Assert.Equal(2000, account.Amount);
	}

	[Fact]
	public void RateUpdaterClientsTest()
	{
		RateUpdater.accounts = DataGenerator.GenerateAccounts(10).ToList();
		decimal?[] expected = RateUpdater.accounts.Select(account => account.Amount * 1.05m).ToArray();
		RateUpdater rateUpdater = new();
		for (int i = 0; i < expected.Length; i++)
		{
			Assert.Equal(expected[i], RateUpdater.accounts[i].Amount);
		}
	}

	[Fact]
	public async Task DispenseCashSimultaneouslyAsyncTest()
	{
		List<Account> accounts = DataGenerator.GenerateAccounts(5).ToList();
		CashDispenserService dispenserService = new(5);
		List<Task<decimal?>> tasks = new();

		foreach (Account account in accounts)
		{
			tasks.Add(dispenserService.DispenseCashAsync(account, 100.00m));
		}

		decimal?[] results = await Task.WhenAll(tasks);

		for (int i = 0; i < accounts.Count(); i++)
		{
			Assert.Equal(accounts[i].Amount, results[i] + 100.00m);
		}
	}
}
