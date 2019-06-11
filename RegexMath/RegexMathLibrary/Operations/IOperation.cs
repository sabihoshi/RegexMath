using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexMathLibrary.Operations
{
    public interface IOperation
    {
        string Pattern { get; }
        Regex Regex { get; }
        bool TryEvaluate(ref string input);
    }
}
