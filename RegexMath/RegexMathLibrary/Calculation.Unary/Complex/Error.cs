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
            operation = operation?.ToLower();

            if (Regex.IsMatch(operation, @"^e((rror|rr)f?|rf)c(Inv|^-1)$"))
                return SpecialFunctions.ErfcInv;

            if (Regex.IsMatch(operation, @"^e((rror|rr)f?|rf)(Inv|^-1)$"))
                return SpecialFunctions.ErfInv;

            if (Regex.IsMatch(operation, @"^e((rror|rr)f?|rf)c$"))
                return SpecialFunctions.Erfc;

            if (Regex.IsMatch(operation, @"^e((rror|rr)f?|rf)$"))
                return SpecialFunctions.Erf;

            throw new InvalidOperationException($"Operation '{operation}' does not exist.");
        }
    }
}