using System;
using MathNet.Numerics;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Binary.Complex
{
    public sealed class Hypotenuse : BinaryCalculation
    {
        public Hypotenuse()
            : base(Pattern, brackets: true) { }

        private static string Pattern { get; } =
            $@"Hypotenuse[(]{Number}, {Number}[)]";

        protected override Func<double, double, double> GetOperation(string operation) => SpecialFunctions.Hypotenuse;
    }
}