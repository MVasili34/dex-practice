using Models;

namespace Services.Storage;

public interface IClientStorage : IStorage<Client>
{
	Dictionary<Client, List<Account>> Data { get; }
    IEnumerable<KeyValuePair<Client, List<Account>>> FilterMethod(string fName, string lName,
        string phone, string info, DateOnly sDate, DateOnly eDate);
    IEnumerable<KeyValuePair<Client, List<Account>>> GetOldestClients();
    IEnumerable<KeyValuePair<Client, List<Account>>> GetYoungestClients();
    int GetAvarageAge();
    void AddAccount(Client client, Account account);
	void UpdateAccount(Client client, int accountNumber, Account account);
	void DeleteAccount(Client client, Account account);
}