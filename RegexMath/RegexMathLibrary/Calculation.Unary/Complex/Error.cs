using System;
using System.Text.RegularExpressions;
using MathNet.Numerics;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Unary.Complex
{
    public sealed class Error : UnaryCalculation
    {
        public Error()
            : base(Pattern, brackets: true) { }

        private static string Pattern { get; } =
            $@"(?<{Token.Operator}>E((rror|rr)f?|rf)c?(Inv|^-1)?)
               [(]{Number}[)]";

        protected override Func<double, double> GetOperation(string operation)
        {
            return operation?.ToLower() switch
            {
                var o when Regex.IsMatch(o, @"^e((rror|rr)f?|rf)c(Inv|^-1)$") => SpecialFunctions.ErfcInv,
                var o when Regex.IsMatch(o, @"^e((rror|rr)f?|rf)(Inv|^-1)$")  => SpecialFunctions.ErfInv,
                var o when Regex.IsMatch(o, @"^e((rror|rr)f?|rf)c$")          => SpecialFunctions.Erfc,
                var o when Regex.IsMatch(o, @"^e((rror|rr)f?|rf)$")           => SpecialFunctions.Erf,
                _ => throw new InvalidOperationException(
                    $"Operation '{operation}' does not exist.")
            };
        }
    }
}