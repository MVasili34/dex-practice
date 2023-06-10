namespace Models
{
	public class Client : Person
	{
		private string connectedCompany = String.Empty;
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

		public override bool Equals(object? obj)
		{
			if (obj is null)
				return false;
			if (!(obj is Client))
				return false;
			var other = obj as Client;
			return other?.connectedCompany == connectedCompany &&
				other?.contactInfo == contactInfo &&
				other.FirstName == FirstName &&
				other.LastName == LastName &&
				other.Phone == Phone &&
				other.Passport == Passport &&
				other.DateOfBirth.CompareTo(DateOfBirth) == 0;
		}
		
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash *= 23 + connectedCompany.GetHashCode();
				hash *= 23 + contactInfo.GetHashCode();
				hash *= 23 + FirstName.GetHashCode();
				hash *= 23 + LastName.GetHashCode();
				hash *= 23 + Phone.GetHashCode();
				hash *= 23 + Passport.GetHashCode();
				hash *= 23 + DateOfBirth.GetHashCode();
				return hash;
			}
		} 

		//метод осуществления явного приведения Клиента к Сотруднику банка
		public static explicit operator Employee(Client client) => new Employee(client.FirstName, client.LastName, 
			client.DateOfBirth, client.Phone, client.Passport, "Стажёр", 1000);
	}
}