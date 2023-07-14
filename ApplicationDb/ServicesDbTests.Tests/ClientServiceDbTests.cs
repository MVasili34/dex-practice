using EntityModels;
using ServicesDb;

namespace ServicesDbTests.Tests;


public class ClientServiceDbTests
{
	[Fact]
	public async void AddingClientTest()
	{
		ClientService service = new(new BankingServiceContext());
		var client = DataGenerator.GenereteClient();

		await service.AddClientAsync(client);

		Assert.Equal(client, service.RetrieveClientAsync(client.ClientId).Result);
	}

	[Fact]
	public void AddAccountClientTest()
	{
		ClientService service = new(new BankingServiceContext());
		var clientId = service.RetrieveAllAsync().Result.First().ClientId;

        int status = service.AddAccount(new Account(clientId, "RUB", 0)).Result;

        Assert.Equal(1, status);
	}

	[Fact]
	public void EditClientByIdTest()
	{
		ClientService service = new(new BankingServiceContext());
		Client client = service.RetrieveAllAsync().Result.First();

	    client.FirstName = "TEST";

		Assert.Equal(client, service.UpdateClientAsync(client.ClientId, client).Result);
	}

	[Fact]
	public void DeleteClientTest()
	{
		ClientService service = new(new BankingServiceContext());
            
		var id = service.RetrieveAllAsync().Result.First().ClientId;

        Assert.True(service.DeleteClientAsync(id).Result);
	}
}