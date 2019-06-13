using RegexMath.Operations;

namespace RegexMath
{
    public static class RegexMath
    {
        private static Parenthesis P { get; } = new Parenthesis();
        private static Exponents E { get; } = new Exponents();
        private static MultiplicationDivision MD { get; } = new MultiplicationDivision();
        private static AdditionSubtraction AS { get; } = new AdditionSubtraction();

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

            if (E.TryEvaluate(ref output))
                output = Calculate(output);

            if (MD.TryEvaluate(ref output))
                output = Calculate(output);

            if (AS.TryEvaluate(ref output))
                output = Calculate(output);

            if (P.TryEvaluate(ref output))
                output = Calculate(output);

            return output;
        }
    }
}