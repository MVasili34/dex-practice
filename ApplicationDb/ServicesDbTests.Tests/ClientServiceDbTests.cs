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
	public async Task AddingClientTest()
	{
		Client client = DataGenerator.GenereteClient();

		await _service.AddClientAsync(client);
		Client? added = await _service.RetrieveClientAsync(client.ClientId);

		Assert.Equal(client, added);
	}

	[Fact]
	public async Task AddAccountClientTest()
	{
		IEnumerable<Client> clients = await _service.RetrieveAllAsync(1);
		Guid clientId = clients.First().ClientId;

        Account? account = await _service.AddAccountAsync(new Account(clientId, "RUB", 0));

        Assert.NotNull(account);
	}

	[Fact]
	public async Task EditClientByIdTest()
	{
        IEnumerable<Client> clients = await _service.RetrieveAllAsync(1);
        Client client = clients.First();

	    client.FirstName = "TEST";
		Client? expected = await _service.UpdateClientAsync(client.ClientId, client);

        Assert.Equal(client, expected);
	}

	[Fact]
	public async Task DeleteClientTest()
	{
        Client client = DataGenerator.GenereteClient();

        await _service.AddClientAsync(client);
		bool? status = await _service.DeleteClientAsync(client.ClientId);

        Assert.True(status);
	}
}