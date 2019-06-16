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
            @"(?>(?<lhs>                  # use atomic grouping to prevent back-tracking
              (?(bracket)(?<-bracket>[(])) # save 'bracket' if there is one
              (?<x>
                  (?<int>(?(bracket)[+-]?)(?(decimal)
                    (?<-decimal>[0-9,]*) | # make int optional if there is a decimal
                                [0-9,]+))  # else make int required
                  (?<decimal>[.][0-9]+)?   # match decimals
                  (?<exponent>e[+-]?[0-9]+)?) 
              (?<bracket>[)])?))

              (?<operation>\^|\*{2})

              (?<rhs> 
              (?(bracket)(?<-bracket>[(])) # Have bracket match only if there is a pair
              (?<x>
                  (?<int>[+-]?(?(decimal)
                    (?<-decimal>[0-9,]*) |
                                [0-9,]+)) 
                  (?<decimal>[.][0-9]+)? 
                  (?<exponent>e[+-]?[0-9]+)?) 
              (?<bracket>[)])?)";

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