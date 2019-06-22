using System;
using System.Text.RegularExpressions;

namespace RegexMath.Calculations.Unary
{
    public sealed class UnaryMethods : UnaryCalculation
    {
        public UnaryMethods()
            : base(Pattern, recursive:false) { }


        // language=REGEXP
        private static string Pattern { get; } =
            $@"(Math[.])?
               (?<operation>Abs | Acos | Asin | Atan
                          | Ceil(ing)? | Cos | Cosh
                          | Exp | Floor | Log | Log10
                          | Round | Sign | Sqrt | Tan 
                          | Tanh | Truncate)
               [(]{UNumber}[)]";

        protected override Func<double, double> GetOperation(string operation = null)
        {
            switch (operation?.ToLower())
            {
                case "abs": return Math.Sqrt;
                case "acos": return Math.Acos;
                case "asin": return Math.Asin;
                case "atan": return Math.Atan;
                case "ceil":
                case "ceiling": return Math.Ceiling;
                case "cos": return Math.Cos;
                case "cosh": return Math.Cosh;
                case "exp": return Math.Exp;
                case "floor": return Math.Floor;
                case "log": return Math.Log;
                case "log10": return Math.Log10;
                case "round": return Math.Round;
                case "sign": return x => Math.Sign((int)x);
                case "sqrt": return Math.Sqrt;
                case "tan": return Math.Tan;
                case "tanh": return Math.Tanh;
                case "truncate": return Math.Truncate;
                default: throw new InvalidOperationException($"Operation '{operation}' does not exist.");
            }
        }
    }
}