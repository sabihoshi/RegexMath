using System;

namespace RegexMath.Calculations.Binary
{
    public sealed class AddSubtract : BinaryCalculation
    {
        public AddSubtract()
            : base(Pattern) { }

        // language=REGEXP
        private static string Operation { get; } =
            @"(?<operation>
                (?(rhs)\k<operation>|[+-])) (?# back-reference operation or capture it)";

        // language=REGEXP
        private static string Pattern { get; } =
            $@"(?>{UNumber}) ({Operation} (?<rhs>{UNumber}))+";

        protected override Func<double, double, double> GetOperation(string operation = null)
        {
            switch (operation)
            {
                case "-": return Subtract;
                case "+": return Add;
                default:  throw new InvalidOperationException();
            }
        }

        private static double Subtract(double x, double y) => x - y;

        private static double Add(double x, double y) => x + y;
    }
}