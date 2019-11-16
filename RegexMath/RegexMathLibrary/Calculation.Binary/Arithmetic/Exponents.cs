using System;
using System.Text.RegularExpressions;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Binary.Arithmetic
{
    public sealed class Exponents : BinaryCalculation
    {
        public Exponents()
            : base(Pattern, RegexOptions.RightToLeft | RegexOptions.IgnoreCase) { }

        private static string Operation { get; } =
            $@"(?<{Token.Operator}>\^|\*{{2}})";

        private new static string Int { get; } =
            $@"(?<{Token.Int}>[+-]?(?({Token.Decimal})
                 (?<-{Token.Decimal}>[0-9,]*) |
                                     [0-9,]+))";

        private new static string Decimal { get; } =
            $@"(?<{Token.Decimal}>[.][0-9]*)?";

        private new static string Number { get; } =
            $@"(?({Token.Bracket})(?<-{Token.Bracket}>[(])+)
               (?<{Token.Number}>{Int}?{Decimal}{Exponent})
               (?<{Token.Bracket}>[)])*";

        private static string Pattern { get; } =
            $@"(?>{Number} {Operation})+ {Number}";

        protected override Func<double, double, double> GetOperation(string operation)
        {
            return (x, y) => Math.Pow(y, x);
        }
    }
}