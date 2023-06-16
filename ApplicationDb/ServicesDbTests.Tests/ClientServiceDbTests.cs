using EntityModels;
using ServicesDb;

namespace ServicesDbTests.Tests
{
	
	public class ClientServiceDbTests
	{
		[Fact]
		public void AddingClientTest()
		{
			ClientService db = new(new BankingServiceContext());
			var someClient = DataGenerator.GenereteClient();

			db.AddClient(someClient);

			Assert.Contains(someClient, db.RetrieveAll());
		}
		[Fact]
		public void GetClientByIdTest()
		{
			ClientService db = new(new BankingServiceContext());
			Assert.Equal(db.RetrieveAll().First().ClientId, db.RetrieveClientById(db.RetrieveAll().First().ClientId)!.ClientId);
		}

		[Fact]
		public void AddAccountClientTest()
		{
			ClientService db = new(new BankingServiceContext());

			Assert.Equal(1, db.AddAccount(new Account(db.RetrieveAll().First().ClientId, "RUB", 0)));
		}

		[Fact]
		public void EditClientByIdTest()
		{
			ClientService db = new(new BankingServiceContext());
			Client client = db.RetrieveClientById(db.RetrieveAll().First().ClientId)!;
			client.FirstName = "TEST";
			Assert.Equal(client, db.EditClient(db.RetrieveAll().First().ClientId, client));
		}

		[Fact]
		public void DeleteClientTest()
		{
			ClientService db = new(new BankingServiceContext());
			Assert.True(db.DeleteClient(db.RetrieveAll().Reverse().First().ClientId));
		}
	}
}