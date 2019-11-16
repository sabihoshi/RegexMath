using System;
using MathNet.Numerics;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Binary.Complex
{
    public sealed class Integral : BinaryCalculation
    {
        public Integral()
            : base(Pattern, brackets: true) { }

        private static string Pattern { get; } =
            $@"E_{Number}[(](?<{Token.Number}>{Int})[)]";

        protected override Func<double, double, double> GetOperation(string operation)
        {
            return (x, y) => SpecialFunctions.ExponentialIntegral(x, (int)y);
        }
    }
}