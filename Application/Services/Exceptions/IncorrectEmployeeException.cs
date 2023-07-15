﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    public class IncorrectEmployeeException : Exception
    {
        public IncorrectEmployeeException() { }
        public IncorrectEmployeeException(string message) : base(message) { }
        public IncorrectEmployeeException(string message, Exception e) : base(message, e) { }
    }
}