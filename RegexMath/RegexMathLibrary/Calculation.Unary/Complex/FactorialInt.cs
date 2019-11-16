using System;
using MathNet.Numerics;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Unary.Complex
{
    public sealed class FactorialInt : UnaryCalculation
    {
        public FactorialInt()
            : base(Pattern, brackets: true) { }

        private static string Pattern { get; } =
            $@"{Int}! |
               (?<{Token.Operator}>Factorial(Ln)?)
               [(]{Int}[)]";

        protected override Func<double, double> GetOperation(string operation)
        {
            switch (operation?.ToLower())
            {
                case "factorialln": return x => SpecialFunctions.Factorial((int)x);
                default:            return x => SpecialFunctions.FactorialLn((int)x);
            }
        }
    }
}