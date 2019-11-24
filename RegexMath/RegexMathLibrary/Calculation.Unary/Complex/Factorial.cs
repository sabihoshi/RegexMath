using System;
using MathNet.Numerics;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Unary.Complex
{
    public sealed class Factorial : UnaryCalculation
    {
        public Factorial()
            : base(Pattern, brackets: true) { }

        private static string Pattern { get; } =
            $@"{Number}! |
               (?<{Token.Operator}>Factorial(Ln)?)
               [(]{Number}[)]";

        protected override Func<double, double> GetOperation(string operation)
        {
            return operation?.ToLower() switch
            {
                "factorialln" => x => SpecialFunctions.GammaLn(x + 1),
                _             => x => SpecialFunctions.Gamma(x + 1)
            };
        }
    }
}