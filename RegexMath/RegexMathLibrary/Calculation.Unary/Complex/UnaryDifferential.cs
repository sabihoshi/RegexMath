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
            return operation?.ToLower() switch
            {
                "i_0" => SpecialFunctions.BesselI0,
                "i_1" => SpecialFunctions.BesselI1,

                "k_0" => SpecialFunctions.BesselK0,
                "k_1" => SpecialFunctions.BesselK1,

                "e^xk_0" => SpecialFunctions.BesselK0e,
                "e^xk_1" => SpecialFunctions.BesselK1e,

                _ => throw new InvalidOperationException($"Operation '{operation}' does not exist.")
            };
        }
    }
}