using System;
using MathNet.Numerics;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Binary.Complex
{
    public class Euler : BinaryCalculation
    {
        public Euler()
            : base(Pattern, brackets: true) { }

        private static string Pattern { get; } =
            $@"(Math[.])?
               (?<operation>Beta(Ln)?|(Ln?)[Bβ])
               [(]{Number}, {Number}[)]";

        protected override Func<double, double, double> GetOperation(string operation)
        {
            switch (operation?.ToLower())
            {
                case "β":
                case "b":
                case "beta": return SpecialFunctions.Beta;

                case "lnβ":
                case "lnb":
                case "betaln": return SpecialFunctions.BetaLn;

                default: throw new InvalidOperationException($"Operation '{operation}' does not exist.");
            }
        }
    }
}
