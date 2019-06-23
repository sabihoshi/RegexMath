using System;
using RegexMath.Calculation.Operation;

namespace RegexMath.Calculation.Binary.Arithmetic
{
    public sealed class MultiplyDivide : BinaryCalculation
    {
        public MultiplyDivide()
            : base(Pattern) { }

        private static string Operation { get; } =
            @"(?(operation)
                (?(multiplication)[*]? | # if operation is multiplication, have it be optional
                  \k<operation>) |       # back-reference operation
                (?<operation>            # save operation if there is none
                  ([/%]            |
                   rem(ain(der)?)? |
                   mod(ul(o|us))?) |
                  (?<multiplication>[*]?)))";

        private static string Pattern { get; } =
            $@"(?>{UNumber})

              ({Operation}

              (?<bracket>[(])*
              (?<x>
                (?<int>
                  (?(multiplication)     # if operation is multiplication
                    (?(bracket)[+-]?) |  # a bracket is needed to allow signed
                               [+-]?)    # else allow signed
                  [0-9,]+)?
                {Decimal}
                {Exponent}
              )
              (?(bracket)(?<-bracket>[)])+))+";

        protected override Func<double, double, double> GetOperation(string operation)
        {
            switch (operation)
            {
                case "/": return Divide;
                case "rem":
                case "%": return Remainder;
                case "mod":
                case "modulo":
                case "modulus":
                    return Modulo;

                default: return Multiply;
            }
        }

        private static double Multiply(double x, double y) => x * y;

        private static double Remainder(double x, double y) => x % y;

        private static double Divide(double x, double y) => x / y;

        private static double Modulo(double x, double y)
        {
            var result = x % y;
            if (result < 0 && y > 0 || result > 0 && y < 0) result += y;
            return result;
        }
    }
}