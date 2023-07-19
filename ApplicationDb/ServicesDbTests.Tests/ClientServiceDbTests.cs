using EntityModels;
using Microsoft.Extensions.DependencyInjection;
using ServicesDb;

namespace ServicesDbTests.Tests;


public class ClientServiceDbTests
{
    private readonly IClientService _service;

	public ClientServiceDbTests()
	{
        this._service = DependencyContainer.Configure()
			.GetService<IClientService>()!;
	}

    [Fact]
	public async void AddingClientTest()
	{
		Client client = DataGenerator.GenereteClient();

		await _service.AddClientAsync(client);
		Client? added = await _service.RetrieveClientAsync(client.ClientId);

		Assert.Equal(client, added);
	}

	[Fact]
	public async void AddAccountClientTest()
	{
		Guid clientId = _service.RetrieveAllAsync(1).Result.First().ClientId;

        Account? account = await _service.AddAccountAsync(new Account(clientId, "RUB", 0));

        Assert.NotNull(account);
	}

	[Fact]
	public async void EditClientByIdTest()
	{
		Client client = _service.RetrieveAllAsync(1).Result.First();

	    client.FirstName = "TEST";
		Client? expected = await _service.UpdateClientAsync(client.ClientId, client);

        Assert.Equal(client, expected);
	}

	[Fact]
	public async void DeleteClientTest()
	{
        Client client = DataGenerator.GenereteClient();

        await _service.AddClientAsync(client);
		bool? status = await _service.DeleteClientAsync(client.ClientId);

        Assert.True(status);
	}
}