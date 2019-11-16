using System;
using MathNet.Numerics;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Binary.Complex
{
    public sealed class BinaryHarmonic : BinaryCalculation
    {
        public BinaryHarmonic()
            : base(Pattern, brackets: true) { }

        private static string Pattern { get; } =
            $@"H_ (?<{Token.Number}>{Int}), {Number} |
               (Gen(eral(ized)?)?)?Harmonic
               [(](?<{Token.Number}>{Int}), {Number}[)]";

        protected override Func<double, double, double> GetOperation(string operation)
        {
            return (x, y) => SpecialFunctions.GeneralHarmonic((int)x, y);
        }
    }
}