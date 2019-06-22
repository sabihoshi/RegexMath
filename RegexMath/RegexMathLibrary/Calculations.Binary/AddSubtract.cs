using System;

namespace RegexMath.Calculations.Binary
{
    public sealed class AddSubtract : BinaryCalculation
    {
        public AddSubtract()
            : base(Pattern) { }

        // language=REGEXP
        private static string Pattern { get; } =
            @"(?>(?<lhs>                      # use atomic grouping to prevent back-tracking
              (?<bracket>[(])*
              (?<x>                           # save 'x' as the full number
                (?<int>[+-]?[0-9,]+)?         # match integer or commas
                (?<decimal>(?(int)
                  (?<-int>([.][0-9]*)?) |     # make decimal optional if there is an 'int'
                           [.][0-9]+) )       # else make decimal required
                (?<exponent>e[+-]?[0-9]+)?)
              (?(bracket)(?<-bracket>[)])+))) # balance brackets

             ((?<operation>
                (?(rhs)\k<operation>|[+-]))   # back-reference operation or capture it

              (?<rhs>(?<bracket>[(])*
              (?<x>
                (?<int>[+-]?[0-9,]+)?
                (?<decimal>(?(int)
                  (?<-int>([.][0-9]*)?) |
                           [.][0-9]+) )
                (?<exponent>e[+-]?[0-9]+)?)
              (?(bracket)(?<-bracket>[)])+)))+";

        protected override Func<double, double, double> GetOperation(string operation = null)
        {
            switch (operation)
            {
                case "-": return Subtract;
                case "+": return Add;
                default:  throw new InvalidOperationException();
            }
        }

        private static double Subtract(double x, double y) => x - y;

        private static double Add(double x, double y) => x + y;
    }
}