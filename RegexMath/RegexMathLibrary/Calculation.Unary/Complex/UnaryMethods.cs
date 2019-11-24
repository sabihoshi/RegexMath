using System;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Unary.Complex
{
    public sealed class UnaryMethods : UnaryCalculation
    {
        public UnaryMethods()
            : base(Pattern, brackets: true) { }

        private static string Pattern { get; } =
            $@"(Math[.])?
               (?<{Token.Operator}>Ceil(ing)?  | Exp 
                          | Floor | Log | Log10 | Round 
                          | Sign | Sqrt | Trunc(ate)?)
               [(]{Number}[)]";

        protected override Func<double, double> GetOperation(string operation)
        {
            return operation?.ToLower() switch
            {
                "exp"      => Math.Exp,
                "floor"    => Math.Floor,
                "log"      => Math.Log,
                "log10"    => Math.Log10,
                "round"    => Math.Round,
                "sqrt"     => Math.Sqrt,
                "cbrt"     => x => Math.Pow(x, 1.0 / 3.0),
                "sign"     => x => Math.Sign((int)x),
                "ceil"     => Math.Ceiling,
                "ceiling"  => Math.Ceiling,
                "trunc"    => Math.Truncate,
                "truncate" => Math.Truncate,
                _          => throw new InvalidOperationException($"Operation '{operation}' does not exist.")
            };
        }
    }
}