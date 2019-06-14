using RegexMath.Operations;

namespace RegexMath
{
    public static class RegexMath
    {
        private static AdditionSubtraction AS { get; } = new AdditionSubtraction();
        private static Exponents E { get; } = new Exponents();
        private static MultiplicationDivision MD { get; } = new MultiplicationDivision();
        private static Parenthesis P { get; } = new Parenthesis();

        private static string Calculate(string input)
        {
            var output = input;

            if (P.TryEvaluate(output, out output))
                Calculate(ref output);

            if (E.TryEvaluate(output, out output))
                Calculate(ref output);

            if (MD.TryEvaluate(output, out output))
                Calculate(ref output);

            if (AS.TryEvaluate(output, out output))
                Calculate(ref output);

            return output;
        }

        private static void Calculate(ref string input)
        {
            input = Calculate(input);
        }

        public static double Evaluate(string input)
        {
            return double.Parse(Calculate(input));
        }

        public static bool TryEvaluate(ref string input, out double result)
        {
            return double.TryParse(Calculate(input), out result);
        }
    }
}