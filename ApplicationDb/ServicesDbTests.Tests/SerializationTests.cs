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
		Client expected = DataGenerator.GenereteClient();

		ExportService<Client>.SerializePerson(expected, path);
		Client? actual = ExportService<Client>.DeSerializePerson(path);

        Assert.Equal(expected, actual);
	}

	[Fact]
	public void SerializeAndDeserializeEmployeeTest()
	{
		string path = Path.Combine(@"D:\", "employee.json");
		Employee expected = DataGenerator.GenerateEmployee();

		ExportService<Employee>.SerializePerson(expected, path);
		Employee? actual = ExportService<Employee>.DeSerializePerson(path);

        Assert.Equal(expected, actual);
	}
}
