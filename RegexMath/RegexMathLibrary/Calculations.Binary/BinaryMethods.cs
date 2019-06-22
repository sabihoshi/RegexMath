using System;
using System.Text.RegularExpressions;

namespace RegexMath.Calculations.Binary
{
    public sealed class BinaryMethods : BinaryCalculation
    {
        public BinaryMethods()
            : base(Pattern, recursive:false) { }

        // language=REGEXP
        private static string Pattern { get; } =
            $@"(Math[.])?
               (?<operation>Atan2 | IEEERemainder
                          | Log | Max | Min | Pow 
                          | Root | Round)
               [(]{Number},{Number}[)]";

        protected override Func<double, double, double> GetOperation(string operation = null)
        {
            switch (operation?.ToLower())
            {
                case "atan2": return Math.Atan2;
                case "ieeeremainder": return Math.IEEERemainder;
                case "log": return Math.Log;
                case "max": return Math.Max;
                case "min": return Math.Min;
                case "pow": return Math.Pow;
                case "root": return (x, y) => Math.Pow(x, 1.0 / y);
                case "round": return (x, y) => Math.Round(x, (int)y);
                default: throw new InvalidOperationException($"Operation '{operation}' does not exist.");
            }
        }
    }
}
