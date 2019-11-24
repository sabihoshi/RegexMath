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
            return operation?.ToLower() switch
            {
                "Γ"          => SpecialFunctions.Gamma,
                "gamma"      => SpecialFunctions.Gamma,
                "lnΓ"        => SpecialFunctions.GammaLn,
                "gammaln"    => SpecialFunctions.GammaLn,
                "ψ"          => SpecialFunctions.DiGamma,
                "digamma"    => SpecialFunctions.DiGamma,
                "ψ^-1"       => SpecialFunctions.DiGammaInv,
                "digammainv" => SpecialFunctions.DiGammaInv,
                _            => throw new InvalidOperationException($"Operation '{operation}' does not exist.")
            };
        }
    }
}