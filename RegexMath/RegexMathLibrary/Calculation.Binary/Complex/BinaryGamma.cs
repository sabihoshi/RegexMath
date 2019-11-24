using System;
using MathNet.Numerics;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Binary.Complex
{
    public sealed class BinaryGamma : BinaryCalculation
    {
        public BinaryGamma()
            : base(Pattern, brackets: true) { }

        private static string Pattern { get; } =
            $@"(Math[.])?
               (?<{Token.Operator}>[γΓQ]|P(^-1)?)
               [(]{Number}, {Number}[)]";

        protected override Func<double, double, double> GetOperation(string operation)
        {
            return operation?.ToLower() switch
            {
                "Γ" => SpecialFunctions.GammaUpperIncomplete,
                "Q" => SpecialFunctions.GammaUpperRegularized,

                "γ"    => SpecialFunctions.GammaLowerIncomplete,
                "P"    => SpecialFunctions.GammaLowerRegularized,
                "P^-1" => SpecialFunctions.GammaLowerRegularizedInv,

                _ => throw new InvalidOperationException($"Operation '{operation}' does not exist.")
            };
        }
    }
}