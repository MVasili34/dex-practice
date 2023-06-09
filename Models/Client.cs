namespace Models
{
	public class Client : Person
	{
		private string? connectedCompany;
		private string contactInfo = String.Empty;
		public Client() { }

		public Client(string FirstName, string LastName, DateOnly DateOfBirth, string Phone, string Passport,
			string? Company, string Info) : base(FirstName, LastName, DateOfBirth, Phone, Passport)
		{
			this.Company = Company;
			this.AdressInfo = Info;
		}

		public string? Company
		{
			get => connectedCompany;
			set => connectedCompany = value;
		}
		public string AdressInfo
		{
			get => contactInfo;
			set => contactInfo = value;
		}

		//метод осуществления явного приведения Клиента к Сотруднику банка
		public static explicit operator Employee(Client client) => new Employee(client.FirstName, client.LastName, 
			client.DateOfBirth, client.Phone, client.Passport, "Стажёр", 1000);
	}
}