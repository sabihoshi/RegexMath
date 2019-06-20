using RegexMath.Calculations;
using RegexMath.Replace;
using System.Collections.Generic;

namespace RegexMath
{
    public static class RegexMath
    {
        private static List<IOperation> Operations { get; } = new List<IOperation>
        {
            new Parenthesis(),
            new Exponents(),
            new MultiplicationDivision(),
            new AdditionSubtraction()
        };

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

            foreach (var operation in Operations)
            {
                if (operation.TryEvaluate(output, out output))
                    Calculate(ref output); 
            }

            return output;
        }

        private static void Calculate(ref string input)
        {
            input = Calculate(input);
        }
    }
}