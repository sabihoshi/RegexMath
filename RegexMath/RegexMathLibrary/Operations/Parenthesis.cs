using System.Text.RegularExpressions;

namespace RegexMath.Operations
{
    public sealed class Parenthesis : Calculation
    {
        /* language=REGEXP */

        protected override string Pattern { get; } =
            @"(?<=^|[(^*/+-]) # Allow start or no 
              (?<bracket>[(])
              (?<x>[+-]?
                  (?<int>(?(bracket)[+-]?)[0-9,]+)? # Integer
                  (?<decimal>(?(int)(?<-int>([.]?[0-9]*)?)|[.][0-9]+)) # Decimal
                  (?<exponent>e[+-]?[0-9]+)?) # Exponent
              (?(bracket)(?<-bracket>[)]))
              (?=$|[)^*/+-])";

        protected override string Replace(Match match)
        {
            return match.Groups["x"].Value;
        }
    }
}