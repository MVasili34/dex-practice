using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    public class NullContractException : Exception
    {
        public NullContractException() { }
        public NullContractException(string message) : base(message) { }
        public NullContractException(string message, Exception e) : base(message, e) { }
    }
}
