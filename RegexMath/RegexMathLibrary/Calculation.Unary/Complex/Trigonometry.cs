using System;
using MathNet.Numerics;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Unary.Complex
{
    public sealed class Trigonometry : UnaryCalculation
    {
        public Trigonometry()
            : base(Pattern, brackets:true) { }

        private static string Pattern { get; } =
            $@"(Math[.])?
               (?<{Token.Operator}>a?(sinc?|cos|tan|cot|sec|csc)h?)
               [(]{Number}[)]";

        protected override Func<double, double> GetOperation(string operation)
        {
            switch (operation?.ToLower())
            {
                // Standard Trigonometry
                case "sin": return Trig.Sin;
                case "cos": return Trig.Cos;
                case "tan": return Trig.Tan;
                case "cot": return Trig.Cot;
                case "sec": return Trig.Sec;
                case "csc": return Trig.Csc;

                case "sinc": return Trig.Sinc;

                case "asin": return Trig.Asin;
                case "acos": return Trig.Acos;
                case "atan": return Trig.Atan;
                case "acot": return Trig.Acot;
                case "asec": return Trig.Asec;
                case "acsc": return Trig.Acsc;

                // Hyperbolic Trigonometry
                case "sinh": return Trig.Sinh;
                case "cosh": return Trig.Cosh;
                case "tanh": return Trig.Tanh;
                case "coth": return Trig.Coth;
                case "sech": return Trig.Sech;
                case "csch": return Trig.Csch;

                case "asinh": return Trig.Asinh;
                case "acosh": return Trig.Acosh;
                case "atanh": return Trig.Atanh;
                case "acoth": return Trig.Acoth;
                case "asech": return Trig.Asech;
                case "acsch": return Trig.Acsch;

                default: throw new InvalidOperationException($"Operation '{operation}' does not exist.");
            }
        }
    }
}
