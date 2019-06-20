using System;

namespace RegexMath.Calculations
{
    public sealed class MultiplicationDivision : CalculationBase
    {
        public MultiplicationDivision()
            : base(Pattern) { }

        // language=REGEXP
        private static string Pattern { get; } =
            @"(?>(?<lhs>                          # use atomic grouping to prevent back-tracking
              (?<bracket>[(])* 
              (?<x>                               # save 'x' as the full number
                (?<int>(?(bracket)[+-]?)[0-9,]+)? # match integer or commas
                (?<decimal>(?(int)
                  (?<-int>([.][0-9]*)?) |         # make decimal optional if there is an 'int'
                           [.][0-9]+) )           # else make decimal required
                (?<exponent>e[+-]?[0-9]+)?)
              (?<-bracket>[)])*))                 # balance brackets

             ((?(operation)
                (?(multiplication)[*]? |          # if operation is multiplication, have it be optional
                  \k<operation>) |                # back-reference operation
                (?<operation>                     # save operation if there is none
                  ([/%]            |
                   rem(ain(der)?)? |
                   mod(ul(o|us))?) |
                  (?<multiplication>[*]?)))

              (?<rhs>(?<bracket>[(])*
              (?<x>
                (?<int>
                  (?(multiplication)              # if operation is multiplication
                    (?(bracket)[+-]?) |           # a bracket is needed to allow signed
                               [+-]?)             # else allow signed
                  [0-9,]+)?
                (?<decimal>(?(int)
                  (?<-int>([.][0-9]*)?) |
                           [.][0-9]+) )
                (?<exponent>e[+-]?[0-9]+)?)
              (?<-bracket>[)])*))+";

        protected override Func<double, double, double> GetOperation(string operation = null)
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