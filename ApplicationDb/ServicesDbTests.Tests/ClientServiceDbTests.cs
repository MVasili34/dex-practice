using EntityModels;
using ServicesDb;

namespace ServicesDbTests.Tests;

public class ClientServiceDbTests
{
    private readonly IClientService _clientService;

	public ClientServiceDbTests(IClientService clientService)
	{
        _clientService = clientService;
	}

    [Fact]
	public async Task AddingClientTest()
	{
		Client client = DataGenerator.GenereteClient();

		await _clientService.AddClientAsync(client);
		Client? added = await _clientService.RetrieveClientAsync(client.ClientId);

		Assert.Equal(client, added);
	}

	[Fact]
	public async Task AddAccountClientTest()
	{
		IEnumerable<Client> clients = await _clientService.RetrieveAllAsync(1);
		Guid clientId = clients.First().ClientId;

        Account? account = await _clientService.AddAccountAsync(new Account(clientId, "RUB", 0));

        Assert.NotNull(account);
	}

	[Fact]
	public async Task EditClientByIdTest()
	{
        IEnumerable<Client> clients = await _clientService.RetrieveAllAsync(1);
        Client client = clients.First();

	    client.FirstName = DateTime.Now.ToString("T");
        Client? expected = await _clientService.UpdateClientAsync(client.ClientId, client);

        Assert.Equal(client, expected);
	}

	[Fact]
	public async Task DeleteClientTest()
	{
        Client client = DataGenerator.GenereteClient();

        await _clientService.AddClientAsync(client);
		bool? status = await _clientService.DeleteClientAsync(client.ClientId);

        Assert.True(status);
	}
}