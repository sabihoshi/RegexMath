using System;

namespace RegexMath.Operations
{
    public sealed class MultiplicationDivision : Calculation
    {
        // language=REGEXP
        protected override string Pattern { get; } =
                @"(?>(?<lhs> # Prevent backtracking so two numbers are required
                  (?<bracket>[(])?
                  (?<x>
                      (?<int>(?(bracket)[+-]?)[0-9,]+)? # Integer
                      (?<decimal>(?(int)(?<-int>([.]?[0-9]*)?)|[.][0-9]+)) #Decimal
                      (?<exponent>e[+-]?[0-9]+)?) # Exponent
                  (?(bracket)(?<-bracket>[)]))))

                  ((?<operation>(?(rhs)(?(operation)\k<operation>|\*?)|(/|\*|%|rem|mod(ulo)?)))?
                    # Save the operation when there is no right hand side
                    # If there is a right hand side, check if there is an operation
                    # Back-reference if there is an operation, otherwise assume it is multiplication

                  (?<rhs>
                  (?<bracket>[(])?
                  (?<x>
                      (?<int>(?(bracket)[+-]?|(?(operation)[+-]?))[0-9,]+)? # Allow positive or negative only if inside a bracket or there is an explicit operation
                      (?<decimal>(?(int)(?<-int>([.]?[0-9]*)?)|[.][0-9]+)) # Decimal
                      (?<exponent>e[+-]?[0-9]+)?)
                  (?(bracket)(?<-bracket>[)]))))+ # Exponent";

        protected override Func<double, double, double> GetOperation(string operation = null)
        {
            switch (operation)
            {
                case "/": return (x, y) => x / y;
                case "rem":
                case "%": return (x, y) => x % y;
                case "mod":
                case "modulo":
                    return (x, y) =>
                    {
                        var result = x % y;
                        if ((result < 0 && y > 0) || (result > 0 && y < 0))
                            result += y;
                        return result;
                    };
                default: return (x, y) => x * y;
            }
        }
    }
}