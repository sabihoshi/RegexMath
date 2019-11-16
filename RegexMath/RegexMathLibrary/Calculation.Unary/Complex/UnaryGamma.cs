using System;
using MathNet.Numerics;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Unary.Complex
{
    public sealed class UnaryGamma : UnaryCalculation
    {
        public UnaryGamma()
            : base(Pattern, brackets: true) { }

        private static string Pattern { get; } =
            $@"(Math[.])?
               (?<{Token.Operator}>Gamma(Ln)?|(Ln)?Γ|ψ(^-1)?|DiGamma(Inv)?)
               [(]{Number}[)]";

        protected override Func<double, double> GetOperation(string operation)
        {
            switch (operation?.ToLower())
            {
                case "Γ":
                case "gamma": return SpecialFunctions.Gamma;

                case "lnΓ":
                case "gammaln": return SpecialFunctions.GammaLn;

                case "ψ":
                case "digamma": return SpecialFunctions.DiGamma;

                case "ψ^-1":
                case "digammainv": return SpecialFunctions.DiGammaInv;

                default: throw new InvalidOperationException($"Operation '{operation}' does not exist.");
            }
        }
    }
}