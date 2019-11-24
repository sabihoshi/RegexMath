using System;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Binary.Arithmetic
{
    public sealed class AddSubtract : BinaryCalculation
    {
        public AddSubtract()
            : base(Pattern) { }

        private static string Operation { get; } =
            $@"(?({Token.Operator})\k<{Token.Operator}> |
                                   (?<{Token.Operator}>[+-]))";

        private static string Pattern { get; } =
            $@"(?>{Number}) ({Operation} {Number})+";

        protected override Func<double, double, double> GetOperation(string operation)
        {
            return operation switch
            {
                "-" => Subtract,
                "+" => Add,

                _ => throw new InvalidOperationException($"Operation '{operation}' does not exist.")
            };
        }

        private static double Subtract(double x, double y) => x - y;

        private static double Add(double x, double y) => x + y;
    }
}