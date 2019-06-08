using System;
using System.Collections.Generic;
using System.Text;

namespace RegexMath.Models
{
    public class Calculation
    {
        public string Regex { get; }
        public Calculate Operation { get;  }
        public delegate string Calculate(string input);

        public Calculation(string regex, Calculate operation)
        {
            Regex = regex;
            Operation = operation;
        }
    }
}
