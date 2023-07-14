using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    public class ClientDoesntExistException : Exception
    {
        public ClientDoesntExistException() { }
        public ClientDoesntExistException(string message) : base(message) { }
        public ClientDoesntExistException(string message, Exception e) : base(message, e) { }
    }
}
