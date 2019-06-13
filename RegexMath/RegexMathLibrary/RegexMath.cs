using System;
using System.Collections.Generic;
using System.Text;
using RegexMathLibrary.Operations;

namespace RegexMathLibrary
{
    public static class RegexMath
    {
        public static Parenthesis Parenthesis { get; } = new Parenthesis();
        public static Exponents Exponents { get; } = new Exponents();
        public static MultiplicationDivision MultiplicationDivision { get; } = new MultiplicationDivision();
        public static AdditionSubtraction AdditionSubtraction { get; } = new AdditionSubtraction();
        public static bool TryEvaluate(ref string input, out double result)
        {
            return double.TryParse(Calculate(input), out result);
        }

        public static double Evaluate(string input)
        {
            return double.Parse(Calculate(input));
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
