using System.Text.RegularExpressions;

namespace RegexMathLibrary.Operations
{
    public sealed class Multiplication : Calculation
    {
        public Multiplication()
        {
            // language=REGEXP
            Operator = @"(?<operation>(?<-number>(?(number||expression)\*?|\*)))";
            Regex = new Regex(Pattern, Options);
        }

        protected override string Operator { get; set; }

        protected override double Operation(double x, double y) => x * y;
    }
}