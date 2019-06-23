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
            $@"(?<operation>Logi(t|stic))
               [(]{Int}[)]";

        protected override Func<double, double> GetOperation(string operation)
        {
            switch (operation?.ToLower())
            {
                case "logistic": return SpecialFunctions.Logistic;
                case "logit": return SpecialFunctions.Logit;

                default: throw new InvalidOperationException($"Operation '{operation}' does not exist.");
            }
        }
    }
}
