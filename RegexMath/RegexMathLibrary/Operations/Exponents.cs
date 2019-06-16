using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace RegexMath.Operations
{
    public sealed class Exponents : Calculation
    {
        public Exponents()
            : base(Pattern, RegexOptions.RightToLeft) { }

        // language=REGEXP
        private static string Pattern { get; } =
            @"(?>(?<number> # user
              (?(bracket)(?<-bracket>[(])) # Have bracket match only if there is a pair
              (?<x>
                  (?<int>[+-]?(?(decimal)(?<-decimal>[0-9,]*)|[0-9,]+)) # Integer
                  (?<decimal>[.][0-9]+)? #Decimal
                  (?<exponent>e[+-]?[0-9]+)?) # Exponent
              (?<bracket>[)])?))

              (?<operation>\^|\*{2})

              (?>(?<number> # Prevent backtracking so two numbers are required
              (?(bracket)(?<-bracket>[(])) # Have bracket match only if there is a pair
              (?<x>
                  (?<int>[+-]?(?(decimal)(?<-decimal>[0-9,]*)|[0-9,]+)) # Integer
                  (?<decimal>[.][0-9]+)? #Decimal
                  (?<exponent>e[+-]?[0-9]+)?) # Exponent
              (?<bracket>[)])?))";

        protected override Func<double, double, double> GetOperation(string operation = null)
        {
            return Math.Pow;
        }

        protected override string Replace(Match match)
        {
            var operation = GetOperation();
            var numbers = match.Groups["x"].Captures.Cast<Capture>()
                               .Select(x => x.Value)
                               .Where(x => double.TryParse(x, out _))
                               .Select(double.Parse)
                               .Reverse();
            return numbers.Aggregate(operation).ToString(CultureInfo.CurrentCulture);
        }
    }
}