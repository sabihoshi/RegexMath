using RegexMath.Calculations;
using RegexMath.Cleanup;
using System.Collections.Generic;

namespace RegexMath
{
    public static class RegexMath
    {
        /// <summary>
        /// The order of operations that run recursively.
        /// Note that pattern is important
        /// </summary>
        private static List<IOperation> Operations { get; } = new List<IOperation>
        {
            // Cleanup
            new Spaces(),
            new Constants(),
            new Parenthesis(),

            // Calculations
            new Exponent(),
            new MultiplyDivide(),
            new AddSubtract()
        };

        private static string Calculate(string input)
        {
            foreach (var operation in Operations)
            {
                if (operation.TryEvaluate(input, out input))
                    input = Calculate(input);
            }

            return input;
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