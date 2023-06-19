using EntityModels;
using ServicesDb;

namespace ServicesDbTests.Tests
{
	
	public class ClientServiceDbTests
	{
		[Fact]
		public async void AddingClientTest()
		{
			ClientService db = new(new BankingServiceContext());
			var someClient = DataGenerator.GenereteClient();

			await db.AddClientAsync(someClient);

			Assert.Contains(someClient, db.RetrieveAllAsync().Result);
		}
		[Fact]
		public void GetClientByIdTest()
		{
			ClientService db = new(new BankingServiceContext());
			Assert.Equal(db.RetrieveAllAsync().Result.First().ClientId, 
				db.RetrieveClientAsync(db.RetrieveAllAsync().Result.First().ClientId)!.Result?.ClientId);
		}

		[Fact]
		public void AddAccountClientTest()
		{
			ClientService db = new(new BankingServiceContext());

			Assert.Equal(1, db.AddAccount(new Account(db.RetrieveAllAsync().Result.First().ClientId, "RUB", 0)).Result);
		}

		[Fact]
		public void EditClientByIdTest()
		{
			ClientService db = new(new BankingServiceContext());
			Client client = db.RetrieveClientAsync(db.RetrieveAllAsync().Result.First().ClientId).Result!;
			client.FirstName = "TEST";
			Assert.Equal(client, db.UpdateClientAsync(client.ClientId, client).Result);
		}

		[Fact]
		public void DeleteClientTest()
		{
			ClientService db = new(new BankingServiceContext());
			Assert.True(db.DeleteClientAsync(db.RetrieveAllAsync().Result.Reverse().First().ClientId).Result);
		}
	}
}