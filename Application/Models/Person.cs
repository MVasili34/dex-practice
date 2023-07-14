using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class Person
	{

		private string fName = string.Empty;
		private string lName = string.Empty;
		private DateOnly birthDate;
		private string phoneNumber = string.Empty;
		private string passportNumber = string.Empty;

		public Person() { }

		public Person(string FirstName, string LastName, DateOnly DateOfBirth, string Phone, string Passport)
		{
			this.FirstName = FirstName;
			this.LastName = LastName;
			this.DateOfBirth = DateOfBirth;
			this.Phone = Phone;
			this.Passport = Passport;
		}

		public string FirstName
		{
			get => fName;
			set => fName = value;
		}

		public string LastName
		{
			get => lName;
			set => lName = value;
		}

		public DateOnly DateOfBirth
		{
			get => birthDate;
			set => birthDate = value;
		}

		public string Phone
		{
			get => phoneNumber;
			set => phoneNumber = value;
		}

		public string Passport
		{
			get => passportNumber;
			set => passportNumber = value;
		}
	}
}
