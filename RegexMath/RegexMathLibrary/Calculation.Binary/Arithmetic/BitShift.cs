using System;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Binary.Arithmetic
{
    public sealed class BitShift : BinaryCalculation
    {
        public BitShift()
            : base(Pattern) { }

        private static string Operation { get; } =
            $@"(?(rhs)\k<{Token.Operator}>|
                      (?<{Token.Operator}>[<>]{{2}}))";


        private static string Pattern { get; } =
            $@"(?>{UNumber}) (?<rhs>{Operation} {Number})+";

        protected override Func<double, double, double> GetOperation(string operation)
        {
            switch (operation)
            {
                case "<<": return (x, y) => (int) x << (int) y;
                case ">>": return (x, y) => (int) x >> (int) y;

                default: throw new InvalidOperationException($"The operation '{operation}' does not exist.");
            }
        }
    }
}