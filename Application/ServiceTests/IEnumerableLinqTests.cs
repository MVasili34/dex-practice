using Services.Storage;
using static Services.TestDataGenerator;
using static ServiceTests.Tests.Startup;

namespace ServiceTests.Tests;

public class IEnumerableLinqTests
{
    private readonly IClientStorage _clientService;
    private readonly IEmployeeStorage _employeeService;
    public IEnumerableLinqTests() 
    {
        _clientService = ConfigureClients(_clients);
        _employeeService = ConfigureEmployees(_employees);
    }

    [Fact]
    public void ClientFilterCheck()
    {
        Dictionary<Client, List<Account>> response = _clientService.FilterMethod("И", "А", "3", "3", 
            new(1990, 01, 01), new(2010, 01, 01)).ToDictionary(pair => pair.Key, pair => pair.Value);

        Client client = _clientService.Data.Keys.First();

        Assert.Contains(client, response.Keys);
    }

    [Fact]
    public void EmployeeFilterCheck()
    {
        List<Employee> response = _employeeService.FilterMethod("И", "А", "3", "3", 
            new(1990, 01, 01), new(2010, 01, 01)).ToList();

        Employee employee = _employeeService.Data.First();

        Assert.Contains(employee, response);
    }

    [Fact]
    public void ClientOldestCheck()
    {
        Dictionary<Client, List<Account>> response = _clientService.GetOldestClients()
            .ToDictionary(pair => pair.Key, pair => pair.Value);

        Client client = _clientService.Data.Keys.Last();

        Assert.Contains(client, response.Keys);
    }

    [Fact]
    public void EmployeeOldestCheck()
    {
        List<Employee> response = _employeeService.GetOldestEmployees().ToList();

        Employee employee = _employeeService.Data[1];

        Assert.Contains(employee, response);
    }

    [Fact]
    public void ClientYoungestCheck()
    {
        Dictionary<Client, List<Account>> response = _clientService.GetYoungestClients()
            .ToDictionary(pair => pair.Key, pair => pair.Value);

        Client client = _clientService.Data.Keys.First();

        Assert.Contains(client, response.Keys);
    }

    [Fact]
    public void EmployeeYoungestCheck()
    {
        List<Employee> response = _employeeService.GetYoungestEmployees().ToList();

        Employee employee = _employeeService.Data[0];

        Assert.Contains(employee, response);
    }

    [Fact]
    public void ClientAvarageAgeCheck()
    {
        int age = _clientService.GetAvarageAge();

        Assert.Equal(34, age);
    }

    [Fact]
    public void EmployeeAvarageAgeCheck()
    {
        int age = _employeeService.GetAvarageAge();

        Assert.Equal(34, age);
    }

    [Fact]
    public static void AddingClientToStorage()
    {
        ClientStorage clientStorage = new();
        Client client = GenerateClients(1).First();

        clientStorage.AddClient(client);

        Assert.Contains(client, clientStorage.Clients.Keys);
    }

    [Fact]
    public static void AddingEmployeeToStorage()
    {
        EmployeeStorage employeeStorage = new();
        Employee employee = GenerateEmployees(1).First();

        employeeStorage.AddEmployee(employee);

        Assert.Contains(employee, employeeStorage.Employees!);
    }

    private Dictionary<Client, List<Account>> _clients = new()
    {
        { new Client("Имя", "Фамилия", new DateOnly(2001, 1, 21), "434413244", "AB234", "NONE", "Адрес"), 
            new List<Account>()
                { new Account(new Currency("RUB", "Рубль"), 20000)} },
        { new Client("Иннокентий", "Петров", new DateOnly(1996, 1, 12), "634643", "AB254", "4234", "Адрес"), 
            new List<Account>()
                { new Account(new Currency("RUB", "Рубль"), 10000)} },
        { new Client("Света", "Петровна", new DateOnly(1967, 3, 19), "32493294", "AB423", "NONE", "Адрес"),
            new List<Account>()
                { new Account(new Currency("RUB", "Рубль"), 10000)} }
    };
    private List<Employee> _employees = new()
    {
        new Employee("Имя", "Фамилия", new DateOnly(2001, 1, 21), "434413244", "AB234", "NONE", 20000),
        new Employee("Света", "Петровна", new DateOnly(1967, 3, 19), "32493294", "AB423", "NONE", 21331),
        new Employee("Иннокентий", "Петров", new DateOnly(1996, 1, 12), "634643", "AB254", "4234", 34224)
    };
}
