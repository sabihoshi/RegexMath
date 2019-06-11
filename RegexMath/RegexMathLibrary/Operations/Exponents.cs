using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace RegexMathLibrary.Operations
{
    public sealed class Exponents : Calculation
    {
        public Exponents()
        {
            // language=REGEXP
            Operator = @"(?<operation>(?(expression)(\^|\*{2})?|(\^|\*{2})))";
            Regex = new Regex(Pattern, Options);
        }

        public override bool TryEvaluate(ref string input)
        {
            if (!Regex.IsMatch(input)) return false;
            input = Regex.Replace(input, match =>
            {
                var numbers = match.Groups["x"].Captures
                                   .Select(y => double.Parse(y.Value))
                                   .Reverse();
                return Calculate(numbers).ToString(CultureInfo.CurrentCulture);
            }, 1);
            return true;
        }
        protected override string Operator { get; set; }
        protected override double Operation(double x, double y) => Math.Pow(x, y);
    }
}
