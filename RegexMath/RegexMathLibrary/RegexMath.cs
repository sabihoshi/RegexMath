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
        public static Multiplication Multiplication { get; } = new Multiplication();
        public static Division Division { get; } = new Division();
        public static Addition Addition { get; } = new Addition();
        public static Subtraction Subtraction { get; } = new Subtraction();
        public static bool TryEvaluate(ref string input, out double result)
        {
            return double.TryParse(Calculate(input), out result);
        }

        public static double Evaluate( string input)
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

            if (Multiplication.TryEvaluate(ref output))
                output = Calculate(output);

            if (Division.TryEvaluate(ref output))
                output = Calculate(output);

            if (Addition.TryEvaluate(ref output))
                output = Calculate(output);

            if (Subtraction.TryEvaluate(ref output))
                output = Calculate(output);

            return output;
        }
    }
}
