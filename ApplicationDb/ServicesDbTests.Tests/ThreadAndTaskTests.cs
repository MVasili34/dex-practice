using ExportTool;
using EntityModels;
using ServicesDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Security.Cryptography;

namespace ServicesDbTests.Tests;

public class ThreadAndTaskTests
{
	[Fact] //параллельный экспорт и импорт клиентов из БД
	public void ParallelExportImportTest()
	{
		var exportCompleted = new ManualResetEvent(false);
		var importCompleted = new ManualResetEvent(false);
		var db = new BankingServiceContext();
		ExportService<Client> exportService = new(Environment.CurrentDirectory, "export.csv");
		ExportService<Client> importService = new(Environment.CurrentDirectory, "import.csv");
		importService.ExportPersons(db.Clients);

		ThreadPool.QueueUserWorkItem(_ =>
		{
			using (var exp = new BankingServiceContext())
			{
				exportService.ExportPersons(exp.Clients);
				exp.Clients.RemoveRange(exp.Clients);
				exp.SaveChanges();
				exportCompleted.Set();
			}
		});
		ThreadPool.QueueUserWorkItem(_ =>
		{
			using (var imp = new BankingServiceContext())
			{
				imp.AddRange(importService.ImportPersons()!);
				imp.SaveChanges();
				importCompleted.Set();
			}
		});
		WaitHandle.WaitAll(new[] { exportCompleted, importCompleted });
		Assert.True(!exportService.ImportPersons()!.Except(db.Clients).Any());
	}

	[Fact] //параллельное начисление денег на один и тот же тестовый счет
	public void ParallelIncreaseTest()
	{
		Account account = new(new Guid(), "USD", 0);

		var amount = account.Amount;
		var sync = new Object();
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

	[Fact] //начисляющий процентную ставку каждому клиенту
	public void RateUpdaterTest()
	{
		RateUpdater.accounts = DataGenerator.GenerateAccounts(10).ToList();
		decimal?[] expected = RateUpdater.accounts.Select(account => account.Amount * 1.05m).ToArray();
		RateUpdater rateUpdater = new RateUpdater();
		for (int i = 0; i < expected.Length; i++)
		{
			Assert.Equal(expected[i], RateUpdater.accounts[i].Amount);
		}
	}

	[Fact] //тест возможности обналичивания средств
	public async Task DispenseCashAsync_ShouldDispenseCash()
	{
		var accounts = DataGenerator.GenerateAccounts(5).ToList();
		var dispenserService = new CashDispenserService(5);
		List<Task<decimal?>> tasks = new();

		foreach (var account in accounts)
		{
			var task = dispenserService.DispenseCashAsync(account, 100.00m);
			tasks.Add(task);
			await task;
		}

		var results = await Task.WhenAll(tasks);

		for (int i = 0; i < accounts.Count; i++)
		{
			Assert.Equal(accounts[i].Amount, results[i] + 100.00m);
		}
	}
}
