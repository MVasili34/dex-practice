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

			await db.AddClient(someClient);

			Assert.Contains(someClient, db.RetrieveAll().Result);
		}
		[Fact]
		public void GetClientByIdTest()
		{
			ClientService db = new(new BankingServiceContext());
			Assert.Equal(db.RetrieveAll().Result.First().ClientId, 
				db.RetrieveClientById(db.RetrieveAll().Result.First().ClientId)!.Result?.ClientId);
		}

		[Fact]
		public void AddAccountClientTest()
		{
			ClientService db = new(new BankingServiceContext());

			Assert.Equal(1, db.AddAccount(new Account(db.RetrieveAll().Result.First().ClientId, "RUB", 0)).Result);
		}

		[Fact]
		public void EditClientByIdTest()
		{
			ClientService db = new(new BankingServiceContext());
			Client client = db.RetrieveClientById(db.RetrieveAll().Result.First().ClientId).Result!;
			client.FirstName = "TEST";
			Assert.Equal(client, db.EditClient(db.RetrieveAll().Result.First().ClientId, client).Result);
		}

		[Fact]
		public void DeleteClientTest()
		{
			ClientService db = new(new BankingServiceContext());
			Assert.True(db.DeleteClient(db.RetrieveAll().Result.Reverse().First().ClientId).Result);
		}
	}
}