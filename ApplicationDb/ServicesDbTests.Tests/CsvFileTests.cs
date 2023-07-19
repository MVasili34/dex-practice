using ExportTool;
using EntityModels;
using ServicesDb;
using Microsoft.Extensions.DependencyInjection;

namespace ServicesDbTests.Tests;

public class CsvFileTests
{
    private readonly IClientService _clientService;
    private readonly IEmployeeService _employeeService;

	public CsvFileTests()
	{
        this._clientService = DependencyContainer.Configure()
            .GetService<IClientService>()!;
        this._employeeService = DependencyContainer.Configure()
            .GetService<IEmployeeService>()!;
    }

    [Fact]
	public async Task ExportImportClientsTest()
	{
		ExportService<Client> exportService = new(Environment.CurrentDirectory, "expimpclient.csv");
		IEnumerable<Client> collection = await _clientService.RetrieveAllAsync(1);

		exportService.ExportPersons(collection);

		Assert.Equal(collection, exportService.ImportPersons()!);
	}

	[Fact]
	public async Task ExportImportEmployeeTest()
	{
		ExportService<Employee> exportService = new(Environment.CurrentDirectory, "expimpemployee.csv");
        IEnumerable<Employee> collection = await _employeeService.RetrieveAllAsync();

        exportService.ExportPersons(collection);

		Assert.Equal(collection, exportService.ImportPersons()!);
	}
}
