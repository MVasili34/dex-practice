using EntityModels;
using ServicesDb;
using ExportTool;

namespace ServicesDbTests.Tests;

public class SerializationTests
{
	[Fact]
	public void SerializeAndDeserializeClientTest()
	{
		string path = Path.Combine(@"D:\", "client.json");
		Client client = DataGenerator.GenereteClient();

		ExportService<Client>.SerializePerson(client, path);
		Client? expected = ExportService<Client>.DeSerializePerson(path);

        Assert.Equal(client, expected);
	}

	[Fact]
	public void SerializeAndDeserializeEmployeeTest()
	{
		string path = Path.Combine(@"D:\", "employee.json");
		Employee employee = DataGenerator.GenerateEmployee();

		ExportService<Employee>.SerializePerson(employee, path);
		Employee? expected = ExportService<Employee>.DeSerializePerson(path);

        Assert.Equal(employee, expected);
	}
}
