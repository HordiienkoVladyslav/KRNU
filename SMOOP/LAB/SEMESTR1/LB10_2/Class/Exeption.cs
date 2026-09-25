using System;
using System.Collections.Generic;
using System.Text;


namespace LB10_2;

public class SuitcaseOverflowException : Exception
{
    public SuitcaseOverflowException(string message) : base(message) { }
}
