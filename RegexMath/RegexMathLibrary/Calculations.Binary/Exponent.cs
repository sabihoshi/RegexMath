using System;
using System.Text.RegularExpressions;

namespace RegexMath.Calculations.Binary
{
    public sealed class Exponent : BinaryCalculation
    {
        public Exponent()
            : base(Pattern, RegexOptions.RightToLeft | RegexOptions.IgnoreCase) { }

        private new static string Decimal { get; } =
            @"(?<decimal>[.][0-9]+)?";

        private static string Exponents { get; } =
            @"(?<exponent>e[+-]?[0-9]+)?)";
        // language=REGEXP
        private static string Pattern { get; } =
            $@"((?>                          # use atomic grouping to prevent back-tracking
               (?(bracket)(?<-bracket>[(])+) # balance brackets
               (?<x>
                 (?<int>(?(bracket)[+-]?)(?(decimal)
                   (?<-decimal>[0-9,]*) |    # make int optional if there is a decimal
                                 [0-9,]+))   # else make int required
                   {Decimal}
                   {Exponents}
               (?<bracket>[)])*)

               (?<operation>\^|\*{{2}}))+

               (?(bracket)(?<-bracket>[(])+)
               (?<x>
                 (?<int>[+-]?(?(decimal)
                   (?<-decimal>[0-9,]*) |
                                [0-9,]+))
                   {Decimal}
                   {Exponents}
               (?<bracket>[)])*";

        protected override Func<double, double, double> GetOperation(string operation = null)
        {
            return (x, y) => Math.Pow(y, x);
        }
    }
}