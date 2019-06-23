using System;
using System.Collections.Generic;
using RegexMath.Calculation.Binary.Arithmetic;
using RegexMath.Calculation.Binary.Complex;
using RegexMath.Calculation.Unary.Arithmetic;
using RegexMath.Calculation.Unary.Complex;
using RegexMath.Operation;

namespace RegexMath
{
    public static class RegexMath
    {
        /// <summary>
        /// The order of operations that run recursively.
        /// Note that the order is important
        /// </summary>
        private static List<IOperation> Operations { get; } = new List<IOperation>
        {
            // Cleanup
            new Abs(),
            new Spaces(),
            new Constants(),
            new Parenthesis(),

            // Binary
            new BinaryGamma(),
            new BinaryHarmonic(),
            new BinaryMethods(),
            new Euler(),
            new Log(),

            // Unary
            new Error(),
            new Sigmoid(),
            new Trigonometry(),
            new UnaryGamma(),
            new UnaryHarmonic(),
            new UnaryMethods(),

            // Arithmetic
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