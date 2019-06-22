using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexMath.Calculations.Unary
{
    public sealed class Factorial : UnaryCalculation
    {
        public Factorial() : base(Pattern) { }

        // language=REGEXP
        private static string Pattern { get; } =
            $@"(?<x>{Int})!";

        protected override Func<double, double> GetOperation(string operation = null)
        {
            return x =>
            {
                var result = (int) x;
                for (int i = result - 1; i > 0; i--) { result *= i; }

                return result;
            };
        }
    }
}
