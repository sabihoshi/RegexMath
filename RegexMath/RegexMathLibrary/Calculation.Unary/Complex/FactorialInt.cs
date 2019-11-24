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
            $@"(?<{Token.Number}>{Int})! |
               (?<{Token.Operator}>Factorial(Ln)?)
               [(](?<{Token.Number}>{Int})[)]";

        protected override Func<double, double> GetOperation(string operation)
        {
            return operation?.ToLower() switch
            {
                "factorialln" => x => SpecialFunctions.FactorialLn((int)x),
                _             => x => SpecialFunctions.Factorial((int)x)
            };
        }
    }
}