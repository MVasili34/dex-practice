using ExportTool;
using EntityModels;
using ServicesDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ServicesDbTests.Tests;

public class ThreadAndTaskTests
{
	[Fact]
	public void ParallelExportImportTest()
	{
		var exportCompleted = new ManualResetEvent(false);
		var importCompleted = new ManualResetEvent(false);
		var db = new BankingServiceContext();
		ExportService<Client> exportService = new(Environment.CurrentDirectory, "export.csv");
		ExportService<Client> importService = new(Environment.CurrentDirectory, "import.csv");
		importService.ExportClients(db.Clients);

		ThreadPool.QueueUserWorkItem(_ =>
		{
			using (var exp = new BankingServiceContext())
			{
				exportService.ExportClients(exp.Clients);
				exp.Clients.RemoveRange(exp.Clients);
				exp.SaveChanges();
				exportCompleted.Set();
			}
		});
		ThreadPool.QueueUserWorkItem(_ =>
		{
			using (var imp = new BankingServiceContext())
			{
				imp.AddRange(importService.ImportClients()!);
				imp.SaveChanges();
				importCompleted.Set();
			}
		});
		WaitHandle.WaitAll(new[] { exportCompleted, importCompleted });
		Assert.True(!exportService.ImportClients()!.Except(db.Clients).Any());
	}
	[Fact]
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
}
