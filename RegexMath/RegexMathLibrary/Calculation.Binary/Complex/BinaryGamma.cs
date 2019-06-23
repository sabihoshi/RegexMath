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
               (?<operation>[γΓQ]|P(^-1)?)
               [(]{Number}, {Number}[)]";

        protected override Func<double, double, double> GetOperation(string operation)
        {
            switch (operation?.ToLower())
            {
                case "Γ":return SpecialFunctions.GammaUpperIncomplete;
                case "Q": return SpecialFunctions.GammaUpperRegularized;

                case "γ": return SpecialFunctions.GammaLowerIncomplete;
                case "P": return SpecialFunctions.GammaLowerRegularized;
                case "P^-1": return SpecialFunctions.GammaLowerRegularizedInv;

                default: throw new InvalidOperationException($"Operation '{operation}' does not exist.");
            }
        }
    }
}
