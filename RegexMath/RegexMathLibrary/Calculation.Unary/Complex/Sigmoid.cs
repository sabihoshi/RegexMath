using System;
using MathNet.Numerics;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Unary.Complex
{
    public sealed class Sigmoid : UnaryCalculation
    {
        public Sigmoid()
            : base(Pattern, brackets: true) { }

        private static string Pattern { get; } =
            $@"(?<{Token.Operator}>Logi(t|stic))
               [(]{Int}[)]";

        protected override Func<double, double> GetOperation(string operation)
        {
            return operation?.ToLower() switch
            {
                "logistic" => SpecialFunctions.Logistic,
                "logit"    => SpecialFunctions.Logit,

                _ => throw new InvalidOperationException($"Operation '{operation}' does not exist.")
            };
        }
    }
}