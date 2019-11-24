using System;
using MathNet.Numerics;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Unary.Complex
{
    public sealed class Trigonometry : UnaryCalculation
    {
        public Trigonometry()
            : base(Pattern, brackets: true) { }

        private static string Pattern { get; } =
            $@"(Math[.])?
               (?<{Token.Operator}>a?(sinc?|cos|tan|cot|sec|csc)h?)
               [(]{Number}[)]";

        protected override Func<double, double> GetOperation(string operation)
        {
            return operation?.ToLower() switch
            {
                // Standard Trigonometry
                "sin" => Trig.Sin,
                "cos" => Trig.Cos,
                "tan" => Trig.Tan,
                "cot" => Trig.Cot,
                "sec" => Trig.Sec,
                "csc" => Trig.Csc,

                "sinc" => Trig.Sinc,

                "asin" => Trig.Asin,
                "acos" => Trig.Acos,
                "atan" => Trig.Atan,
                "acot" => Trig.Acot,
                "asec" => Trig.Asec,
                "acsc" => Trig.Acsc,

                // Hyperbolic Trigonometry
                "sinh" => Trig.Sinh,
                "cosh" => Trig.Cosh,
                "tanh" => Trig.Tanh,
                "coth" => Trig.Coth,
                "sech" => Trig.Sech,
                "csch" => Trig.Csch,

                "asinh" => Trig.Asinh,
                "acosh" => Trig.Acosh,
                "atanh" => Trig.Atanh,
                "acoth" => Trig.Acoth,
                "asech" => Trig.Asech,
                "acsch" => Trig.Acsch,

                _ => throw new InvalidOperationException($"Operation '{operation}' does not exist.")
            };
        }
    }
}