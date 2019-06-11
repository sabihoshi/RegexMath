using System.Text.RegularExpressions;

namespace RegexMathLibrary.Operations
{
    public sealed class Division : Calculation
    {
        public Division()
        {
            // language=REGEXP
            Operator = @"(?<operation>(?(expression)/?|/))";
            Regex = new Regex(Pattern, Options);
        }

        protected override string Operator { get; set; }

        protected override double Operation(double x, double y) => x / y;
    }
}