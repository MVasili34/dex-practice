using ExportTool;
using EntityModels;
using ServicesDb;

namespace ServicesDbTests.Tests;

public class CsvFileTests
{
    private readonly IClientService _clientService;
    private readonly IEmployeeService _employeeService;

	public CsvFileTests(IClientService clientService, IEmployeeService employeeService)
	{
        _clientService = clientService;
        _employeeService = employeeService;
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
        IEnumerable<Employee> collection = await _employeeService.RetrieveAllAsync(1);

        exportService.ExportPersons(collection);

		Assert.Equal(collection, exportService.ImportPersons()!);
	}
}
