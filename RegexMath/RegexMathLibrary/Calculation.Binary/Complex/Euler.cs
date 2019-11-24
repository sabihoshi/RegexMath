using System;
using MathNet.Numerics;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Binary.Complex
{
    public class Euler : BinaryCalculation
    {
        public Euler()
            : base(Pattern, brackets: true) { }

        private static string Pattern { get; } =
            $@"(Math[.])?
               (?<{Token.Operator}>Beta(Ln)?|(Ln?)[Bβ])
               [(]{Number}, {Number}[)]";

        protected override Func<double, double, double> GetOperation(string operation)
        {
            return operation?.ToLower() switch
            {
                "β"      => SpecialFunctions.Beta,
                "b"      => SpecialFunctions.Beta,
                "beta"   => SpecialFunctions.Beta,
                "lnβ"    => SpecialFunctions.BetaLn,
                "lnb"    => SpecialFunctions.BetaLn,
                "betaln" => SpecialFunctions.BetaLn,
                _        => throw new InvalidOperationException($"Operation '{operation}' does not exist.")
            };
        }
    }
}