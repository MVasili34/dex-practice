using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
	public class Below18Exception : Exception
	{
		public Below18Exception() { }
		public Below18Exception(string message) : base(message) { }
		public Below18Exception(string message, Exception e) : base(message, e) { }
	}
	public class ClientDoesntExistException : Exception
	{
		public ClientDoesntExistException() { }
		public ClientDoesntExistException(string message) : base(message) { }
		public ClientDoesntExistException(string message, Exception e) : base(message, e) { }
	}
	public class PassportNullException : Exception 
	{
		public PassportNullException() { }
		public PassportNullException(string message) : base(message) { }
		public PassportNullException(string message, Exception e) : base(message, e) { }
	}
	public class IncorrectAccountException : Exception 
	{
		public IncorrectAccountException() { }
		public IncorrectAccountException(string message) : base(message) { }
		public IncorrectAccountException(string message, Exception e) : base(message, e) { }
	}
	public class NullContractException : Exception 
	{
		public NullContractException() { }
		public NullContractException(string message) : base(message) { }
		public NullContractException(string message, Exception e) : base(message, e) { }
	}
	public class IncorrectEmployeeException : Exception
	{
		public IncorrectEmployeeException() { }
		public IncorrectEmployeeException(string message) : base(message) { }
		public IncorrectEmployeeException(string message, Exception e) : base(message, e) { }
	}
}
