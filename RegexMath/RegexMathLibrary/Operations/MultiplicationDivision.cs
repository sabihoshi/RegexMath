using System;

namespace RegexMathLibrary.Operations
{
    public sealed class MultiplicationDivision : Calculation
    {
        // language=REGEXP
        protected override string Pattern { get; } =
            @"(?>(?<number> # Prevent backtracking so two numbers are required
              (?<bracket>[(])?
              (?<x>
                  (?<int>(?(bracket)[+-]?)[0-9,]+)? # Integer
                  (?<decimal>(?(int)(?<-int>([.]?[0-9]*)?)|[.][0-9]+)) #Decimal
                  (?<exponent>e[+-]?[0-9]+)?) # Exponent
              (?(bracket)(?<-bracket>[)]))))

              (?<operation>/|\*)? # Optional because multiplication can have no symbol

              (?<number>
              (?<bracket>[(])?
              (?<x>
                  (?<int>(?(bracket)[+-]?|(?(operation)[+-]?))[0-9,]+)? # Allow positive or negative only if inside a bracket or there is an explicit operation
                  (?<decimal>(?(int)(?<-int>([.]?[0-9]*)?)|[.][0-9]+)) # Decimal
                  (?<exponent>e[+-]?[0-9]+)?)
              (?(bracket)(?<-bracket>[)]))) # Exponent";

        protected override Func<double, double, double> GetOperation(string operation = null)
        {
            if (operation == "/")
                return Divide;
            return Multiply;
        }

        private static double Divide(double x, double y) => x / y;

        private static double Multiply(double x, double y) => x * y;
    }
}