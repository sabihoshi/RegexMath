using System;
using System.Collections.Generic;
using RegexMath.Calculation.Binary.Arithmetic;
using RegexMath.Calculation.Binary.Complex;
using RegexMath.Calculation.Operation;
using RegexMath.Calculation.Unary.Arithmetic;
using RegexMath.Calculation.Unary.Complex;

namespace RegexMath
{
    public static class RegexMath
    {
        /// <summary>
        ///     The order of operations that run recursively.
        ///     Note that the order is important
        /// </summary>
        private static List<IOperation> Operations { get; } = new List<IOperation>
        {
            // Cleanup
            new Spaces(),
            new Abs(),
            new Parenthesis(),
            new Signs(),

            // Binary
            new Euler(),
            new Hypotenuse(),
            new Integral(),
            new Log(),
            new BinaryDifferential(),
            new BinaryGamma(),
            new BinaryHarmonic(),
            new BinaryMethods(),

            // Unary
            new Error(),
            new Sigmoid(),
            new Trigonometry(),
            new UnaryDifferential(),
            new UnaryGamma(),
            new UnaryHarmonic(),
            new UnaryMethods(),

            // Special
            new Constants(),

            // Arithmetic
            new FactorialInt(),
            new Factorial(),
            new Exponents(),
            new MultiplyDivide(),
            new AddSubtract(),
            new BitShift()
        };

        private static string Calculate(string input)
        {
            foreach (IOperation operation in Operations)
            {
                if (operation.TryEvaluate(input, out input))
                {
                    Console.WriteLine(input);
                    input = Calculate(input);
                }
            }

            return input;
        }

        public static double Evaluate(string input) => double.Parse(Calculate(input));

        public static bool TryEvaluate(string input, out double result) =>
            double.TryParse(Calculate(input), out result);
    }
}