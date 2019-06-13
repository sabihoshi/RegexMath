using RegexMath.Operations;

namespace RegexMath
{
    public static class RegexMath
    {
        private static Parenthesis Parenthesis { get; } = new Parenthesis();
        private static Exponents Exponents { get; } = new Exponents();
        private static MultiplicationDivision MultiplicationDivision { get; } = new MultiplicationDivision();
        private static AdditionSubtraction AdditionSubtraction { get; } = new AdditionSubtraction();

        public static double Evaluate(string input)
        {
            return double.Parse(Calculate(input));
        }

        public static bool TryEvaluate(ref string input, out double result)
        {
            return double.TryParse(Calculate(input), out result);
        }

        private static string Calculate(string input)
        {
            var output = input;

            if (Parenthesis.TryEvaluate(ref output))
                output = Calculate(output);

            if (Exponents.TryEvaluate(ref output))
                output = Calculate(output);

            if (MultiplicationDivision.TryEvaluate(ref output))
                output = Calculate(output);

            if (AdditionSubtraction.TryEvaluate(ref output))
                output = Calculate(output);

            return output;
        }
    }
}