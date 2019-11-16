using System;
using MathNet.Numerics;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Unary.Complex
{
    public sealed class UnaryDifferential : UnaryCalculation
    {
        public UnaryDifferential()
            : base(Pattern, brackets: true) { }

        private static string Pattern { get; } =
            $@"(?<{Token.Operator}>((e^x)?K|I)_[0-1])
               [(]{Number}[)]";

        protected override Func<double, double> GetOperation(string operation)
        {
            switch (operation?.ToLower())
            {
                case "i_0": return SpecialFunctions.BesselI0;
                case "i_1": return SpecialFunctions.BesselI1;

                case "k_0": return SpecialFunctions.BesselK0;
                case "k_1": return SpecialFunctions.BesselK1;

                case "e^xk_0": return SpecialFunctions.BesselK0e;
                case "e^xk_1": return SpecialFunctions.BesselK1e;

                default: throw new InvalidOperationException($"Operation '{operation}' does not exist.");
            }
        }
    }
}
