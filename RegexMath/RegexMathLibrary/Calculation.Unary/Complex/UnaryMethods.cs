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
            switch (operation?.ToLower())
            {
                case "exp": return Math.Exp;
                case "floor": return Math.Floor;
                case "log": return Math.Log;
                case "log10": return Math.Log10;
                case "round": return Math.Round;
                case "sqrt": return Math.Sqrt;
                case "cbrt": return x => Math.Pow(x, 1.0 / 3.0);
                case "sign": return x => Math.Sign((int) x);

                case "ceil":
                case "ceiling": return Math.Ceiling;

                case "trunc":
                case "truncate": return Math.Truncate;

                default: throw new InvalidOperationException($"Operation '{operation}' does not exist.");
            }
        }
    }
}