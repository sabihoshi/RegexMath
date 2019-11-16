using System;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Unary.Arithmetic
{
    public sealed class Abs : UnaryCalculation
    {
        public Abs()
            : base(Pattern, brackets: true) { }

        private static string Pattern { get; } =
            $@"[|]{Number}[|] |
               (det|abs)[(]{Number}[)]";

        protected override Func<double, double> GetOperation(string operation) => Math.Abs;
    }
}