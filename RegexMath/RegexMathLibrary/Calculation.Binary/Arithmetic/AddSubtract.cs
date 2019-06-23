using System;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Binary.Arithmetic
{
    public sealed class AddSubtract : BinaryCalculation
    {
        public AddSubtract()
            : base(Pattern) { }

        private static string Operation { get; } =
            @"(?<operation>
                (?(rhs)\k<operation>|[+-])) (?# back-reference operation or capture it)";

        private static string Pattern { get; } =
            $@"(?>{Number}) ({Operation} (?<rhs>{Number}))+";

        protected override Func<double, double, double> GetOperation(string operation)
        {
            switch (operation)
            {
                case "-": return Subtract;
                case "+": return Add;

                default: throw new InvalidOperationException($"Operation '{operation}' does not exist.");
            }
        }

        private static double Subtract(double x, double y) => x - y;

        private static double Add(double x, double y) => x + y;
    }
}