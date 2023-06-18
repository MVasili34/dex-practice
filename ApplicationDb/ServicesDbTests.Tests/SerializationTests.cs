using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModels;
using ServicesDb;
using ExportTool;
using System.IO;

namespace ServicesDbTests.Tests;

public class SerializationTests
{
	[Fact]
	public void SerializeAndDeserializeClientTest()
	{
		string path = Path.Combine(@"D:\", "client.json");
		Client client = DataGenerator.GenereteClient();
		ExportService<Client>.SerializePerson(client, path);
		Assert.Equal(client, ExportService<Client>.DeSerializePerson(path)!.Result);
	}

	[Fact]
	public void SerializeAndDeserializeEmployeeTest()
	{
		string path = Path.Combine(@"D:\", "employee.json");
		Employee employee = DataGenerator.GenerateEmployee();
		ExportService<Employee>.SerializePerson(employee, path);
		Assert.Equal(employee, ExportService<Employee>.DeSerializePerson(path)!.Result);
	}
}
