using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    public class PassportNullException : Exception
    {
        public PassportNullException() { }
        public PassportNullException(string message) : base(message) { }
        public PassportNullException(string message, Exception e) : base(message, e) { }
    }
}
