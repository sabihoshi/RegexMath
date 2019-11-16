using System;
using MathNet.Numerics;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Unary.Complex
{
    public sealed class UnaryHarmonic : UnaryCalculation
    {
        public UnaryHarmonic()
            : base(Pattern, brackets: true) { }

        private static string Pattern { get; } =
            $@"H_         (?<{Token.Number}>{Int}) |
               Harmonic[(](?<{Token.Number}>{Int})[)]";

        protected override Func<double, double> GetOperation(string operation)
        {
            return x => SpecialFunctions.Harmonic((int)x);
        }
    }
}