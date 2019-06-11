using System.Text.RegularExpressions;

namespace RegexMathLibrary.Operations
{
    public sealed class Subtraction : Calculation
    {
        public Subtraction()
        {
            // language=REGEXP
            Operator = @"(?<operation>(?(expression)-?|-))";
            /* language=REGEXP */
            Regex = new Regex("(?<![%*/)][+-]?)(?<=^|[(+-])"
                            + Pattern
                            + "(?=$|[)+-])(?![%*/(^])", Options);
        }

        protected override string Operator { get; set; }
        protected override double Operation(double x, double y) => x - y;
    }
}