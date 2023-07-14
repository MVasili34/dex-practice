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
}
