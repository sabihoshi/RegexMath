using System;

namespace RegexMath.Operations
{
    public sealed class MultiplicationDivision : Calculation
    {
        // language=REGEXP
        protected override string Pattern { get; } =
                @"(?>                                 # Prevent backtracking so two numbers are required
                  (?<lhs>(?<bracket>[(])?             # save 'bracket' if there is one

                  (?<x>                               # save 'x' as the full number

                    (?<int>(?(bracket)[+-]?)[0-9,]+)? # match integer or commas
                     
                    (?<decimal>(?(int)                   
                      (?<-int>([.][0-9]*)?) |         # make decimal optional if there is an 'int'
                               [.][0-9]+) )           # else make decimal required
                    
                    (?<exponent>(e[+-]?[0-9]+)?)) 

                  (?(bracket)(?<-bracket>[)]))))      # if there is an opening bracket, include a closing one

                 ((?<operation>(?(rhs)
                    (?(operation)\k<operation>|\*?) |                                     
                    (/|\*|%|rem|mod(ulo)?)))?
                    
                  (?<rhs>(?<bracket>[(])?
                  (?<x>

                      (?<int>(?(bracket)[+-]?|(?(operation)[+-]?)) # allow positive or negative if inside a bracket or there is an operation
                        [0-9,]+)? 
                      (?<decimal>(?(int)(?<-int>([.]?[0-9]*)?)|[.][0-9]+)) # Decimal
                      (?<exponent>e[+-]?[0-9]+)?)
                  (?(bracket)(?<-bracket>[)]))))+ # Exponent";

        protected override Func<double, double, double> GetOperation(string operation = null)
        {  
            switch (operation)
            {
                case "/": return (x, y) => x / y;
                case "rem":
                case "%": return (x, y) => x % y;
                case "mod":
                case "modulo":
                    return (x, y) =>
                    {
                        var result = x % y;
                        if ((result < 0 && y > 0) || (result > 0 && y < 0))
                            result += y;
                        return result;
                    };
                default: return (x, y) => x * y;
            }
        }
    }
}