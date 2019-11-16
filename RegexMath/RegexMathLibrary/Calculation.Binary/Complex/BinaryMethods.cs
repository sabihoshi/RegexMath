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
            switch (operation?.ToLower())
            {
                case "atan2":         return Math.Atan2;
                case "ieeeremainder": return Math.IEEERemainder;
                case "max":           return Math.Max;
                case "min":           return Math.Min;
                case "pow":           return Math.Pow;
                case "root":          return (x, y) => Math.Pow(x, 1.0 / y);
                case "round":         return (x, y) => Math.Round(x, (int) y);

                default: throw new InvalidOperationException($"Operation '{operation}' does not exist.");
            }
        }
    }
}