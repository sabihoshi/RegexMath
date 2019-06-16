using System;

namespace RegexMath.Operations
{
    public sealed class AdditionSubtraction : Calculation
    {
        public AdditionSubtraction()
            : base(Pattern) { }

        // language=REGEXP
        private static string Pattern { get; } =
            @"(?<=^|[(+-])                        # make sure to start at a lower order
              (?>(?<lhs>                          # use atomic grouping to prevent back-tracking
              (?<bracket>[(])?                    # save 'bracket' if there is one
              (?<x>                               # save 'x' as the full number
                (?<int>(?(bracket)[+-]?)[0-9,]+)? # match integer or commas
                (?<decimal>(?(int)
                  (?<-int>([.][0-9]*)?) |         # make decimal optional if there is an 'int'
                           [.][0-9]+) )           # else make decimal required
                (?<exponent>e[+-]?[0-9]+)?)
              (?(bracket)(?<-bracket>[)]))))      # if there is an opening bracket, include a closing one

             ((?<operation>
                (?(rhs)\k<operation>|[+-]))    # back-reference operation or capture it

              (?<rhs>(?<bracket>[(])?
              (?<x>
                (?<int>[+-]?[0-9,]+)?
                (?<decimal>(?(int)
                  (?<-int>([.][0-9]*)?) |
                           [.][0-9]+) )
                (?<exponent>e[+-]?[0-9]+)?)
              (?(bracket)(?<-bracket>[)]))))+
              (?=$|[)+-])                         # make sure to end at a lower order";

        protected override Func<double, double, double> GetOperation(string operation = null)
        {
            if (operation == "+")
                return (x, y) => x + y;
            return (x, y) => x - y;
        }
    }
}