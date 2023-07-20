using Xunit.Abstractions;

namespace ServicesDbTests.Tests;

public class AsyncTaskTests
{
	private readonly ITestOutputHelper _output;

	public AsyncTaskTests(ITestOutputHelper output)
	{
		_output = output;
	}

	[Fact]
	public void NonAsyncTest()
	{
		ThreadPool.SetMaxThreads(10, 10);
		for (int i = 0; i < 15; i++) 
		{
			Task.Run(() =>
			{
				_output.WriteLine($"Запуск задачи");
				OutputTreadInfo();
				Thread.Sleep(20000);
				_output.WriteLine($"Завершение задачи");
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
				_output.WriteLine($"Асинхроннная задача запущена");
				OutputTreadInfo();
				await Task.Delay(2000);
				_output.WriteLine($"Асинхроннная задача завершена");
			});

			await Task.Delay(1000); // Задержка между запусками задач
		}
	}

	private void OutputTreadInfo()
	{
		Thread tread = Thread.CurrentThread;
		ThreadPool.GetAvailableThreads(out int workerThreads, out int ioThreads);
		_output.WriteLine($"Поток: {tread.ManagedThreadId}, Приоритет: {tread.Priority}; " +
			$"Worker: {workerThreads}, Входные/выходные потоки: {ioThreads}");
	}

}
