using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace ServicesDbTests.Tests;

public class AsyncTaskTests
{
	private readonly ITestOutputHelper Output;

	public AsyncTaskTests(ITestOutputHelper Output)
	{
		this.Output = Output;
	}

	[Fact]
	public void NonAsyncTest()
	{
		ThreadPool.SetMaxThreads(10, 10);
		for (int i = 0; i < 15; i++) 
		{
			Task.Run(() =>
			{
				Output.WriteLine($"Запуск задачи");
				OutputTreadInfo();
				Thread.Sleep(20000);
				Output.WriteLine($"Завершение задачи");
			});
			Thread.Sleep(1000);
		}
	}

	[Fact]
	public async Task AsyncTest()
	{
		ThreadPool.SetMaxThreads(10, 10);

		for (int i = 0; i < 15; i++)
		{
			_ = Task.Run(async () =>
			{
				Output.WriteLine($"Асинхроннная задача запущена");
				OutputTreadInfo();
				await Task.Delay(2000);
				Output.WriteLine($"Асинхроннная задача завершена");
			});

			await Task.Delay(1000); // Задержка между запусками задач
		}
	}

	private void OutputTreadInfo()
	{
		Thread t = Thread.CurrentThread;
		int workerThreads, ioThreads;
		ThreadPool.GetAvailableThreads(out workerThreads, out ioThreads);
		Output.WriteLine($"Поток: {t.ManagedThreadId}, Приоритет: {t.Priority}; " +
			$"Worker: {workerThreads}, Входные/выходные потоки: {ioThreads}");
	}

}
