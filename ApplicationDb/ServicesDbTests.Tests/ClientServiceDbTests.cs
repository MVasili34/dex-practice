using EntityModels;
using ServicesDb;

namespace ServicesDbTests.Tests;


public class ClientServiceDbTests
{
    ClientService service = new(new BankingServiceContext());

    [Fact]
	public async void AddingClientTest()
	{
		var client = DataGenerator.GenereteClient();

		await service.AddClientAsync(client);

		Assert.Equal(client, service.RetrieveClientAsync(client.ClientId).Result);
	}

	[Fact]
	public void AddAccountClientTest()
	{
		var clientId = service.RetrieveAllAsync().Result.First().ClientId;

        int status = service.AddAccount(new Account(clientId, "RUB", 0)).Result;

        Assert.Equal(1, status);
	}

	[Fact]
	public void EditClientByIdTest()
	{
		Client client = service.RetrieveAllAsync().Result.First();

	    client.FirstName = "TEST";

		Assert.Equal(client, service.UpdateClientAsync(client.ClientId, client).Result);
	}

	[Fact]
	public async void DeleteClientTest()
	{
        var client = DataGenerator.GenereteClient();

        await service.AddClientAsync(client);

        Assert.True(service.DeleteClientAsync(client.ClientId).Result);
	}
}