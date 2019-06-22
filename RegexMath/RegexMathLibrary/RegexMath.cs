using System;
using RegexMath.Calculations.Binary;
using RegexMath.Cleanup;
using System.Collections.Generic;
using RegexMath.Calculations.Unary;

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

            // Methods
            new UnaryMethods(),
            new BinaryMethods(),

            // Calculations
            new Factorial(),
            new Exponent(),
            new MultiplyDivide(),
            new AddSubtract(),
            new BitShift()
        };

        private static string Calculate(string input)
        {
            foreach (var operation in Operations)
            {
                if (operation.TryEvaluate(input, out input))
                {
                    Console.WriteLine(input);
                    input = Calculate(input);
                }
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