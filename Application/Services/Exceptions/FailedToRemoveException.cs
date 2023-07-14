using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    public class FailedToRemoveException : Exception
    {
        public FailedToRemoveException() { }
        public FailedToRemoveException(string message) : base(message) { }
        public FailedToRemoveException(string message, Exception e) : base(message, e) { }
    }
}
