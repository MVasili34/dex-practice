using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    public class IncorrectAccountException : Exception
    {
        public IncorrectAccountException() { }
        public IncorrectAccountException(string message) : base(message) { }
        public IncorrectAccountException(string message, Exception e) : base(message, e) { }
    }
}
