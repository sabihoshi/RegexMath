using System;

namespace RegexMath.Operations
{
    public sealed class AdditionSubtraction : Calculation
    {
        // language=REGEXP
        protected override string Pattern { get; } =
            @"(?<=^|[(+-]) # Allow only matches that are at the end or no higher precedence
              (?>(?<lhs> # Prevent backtracking so two numbers are required
              (?<bracket>[(])?
              (?<x>
                  (?<int>[+-]?[0-9,]+)? # Integer
                  (?<decimal>(?(int)(?<-int>([.]?[0-9]*)?)|[.][0-9]+)) # Decimal
                  (?<exponent>e[+-]?[0-9]+)?) # Exponent
              (?(bracket)(?<-bracket>[)])))) # Have bracket match only if there is a pair

              ((?<operation>(?(rhs)\k<operation>|(-|\+))) # Back-reference operation if there is an existing operand

              (?<rhs>
              (?<bracket>[(])?
              (?<x>
                  (?<int>[+-]?[0-9,]+)? # Integer
                  (?<decimal>(?(int)(?<-int>([.]?[0-9]*)?)|[.][0-9]+)) # Decimal
                  (?<exponent>e[+-]?[0-9]+)?) # Exponent
              (?(bracket)(?<-bracket>[)]))))+ # Have 1 or more operations
              (?=$|[)+-]) # Allow only matches that are at the end or no higher precedence";


        protected override Func<double, double, double> GetOperation(string operation = null)
        {
            if (operation == "+")
                return (x, y) => x + y;
            return (x, y) => x - y;
        }
    }
}