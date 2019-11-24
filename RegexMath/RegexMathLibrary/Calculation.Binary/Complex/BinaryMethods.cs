using System;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Binary.Complex
{
    public sealed class BinaryMethods : BinaryCalculation
    {
        public BinaryMethods()
            : base(Pattern) { }

        private static string Pattern { get; } =
            $@"(Math[.])?
               (?<{Token.Operator}>Atan2 | IEEERemainder
                          | Max | Min | Pow
                          | Root | Round)
               [(]{Number}, {Number}[)]";

        protected override Func<double, double, double> GetOperation(string operation)
        {
            return operation?.ToLower() switch
            {
                "atan2"         => Math.Atan2,
                "ieeeremainder" => Math.IEEERemainder,
                "max"           => Math.Max,
                "min"           => Math.Min,
                "pow"           => Math.Pow,
                "root"          => (x, y) => Math.Pow(x, 1.0 / y),
                "round"         => (x, y) => Math.Round(x, (int)y),

                _ => throw new InvalidOperationException($"Operation '{operation}' does not exist.")
            };
        }
    }
}