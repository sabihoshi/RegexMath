using System;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Binary.Arithmetic
{
    public sealed class BitShift : BinaryCalculation
    {
        public BitShift()
            : base(Pattern) { }

        private static string Operation { get; } =
            @"(?<operation>
                (?(rhs)\k<operation>|[<>]{2})) (?# back-reference operation or capture it)";

        private static string Pattern { get; } =
            $@"(?>{Number}) ({Operation} (?<rhs>{Number}))+";

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